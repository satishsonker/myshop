using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using System.Data.Entity;

namespace Myshop.Areas.Global.Models
{
    public class AdminDetails
    {
        MyshopDb myshop;

        public List<ErrorLog> GetErrorLog(bool all)
        {
            try
            {
                myshop = new MyshopDb();
                List<ErrorLog> log = myshop.ErrorLogs.Where(x => x.IsDeleted == false && (all == true || x.IsResolved == false)).ToList();
                return log;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Enums.CrudStatus UpdateErrorLog(int ErrorId)
        {
            try
            {
                myshop = new MyshopDb();
                var log = myshop.ErrorLogs.Where(x => x.IsDeleted == false && x.Id.Equals(ErrorId)).FirstOrDefault();
                if (log == null)
                {
                    return Enums.CrudStatus.NotExist;
                }
                else
                {
                    int result = 0;
                    log.IsResolved = true;
                    log.ModifiedDate = DateTime.Now;
                    myshop.Entry(log).State = EntityState.Modified;
                    result = myshop.SaveChanges();
                    return result > 0 ? Utility.CrudStatus(result, Enums.CrudType.Update) : Enums.CrudStatus.NoEffect;
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }

        public Enums.CrudStatus ResetUserPassword(int _userId)
        {
            try
            {
                myshop = new MyshopDb();
                var _user = myshop.Gbl_Master_User.Where(x => x.IsDeleted == false && x.UserId.Equals(_userId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (_user == null)
                {
                    return Enums.CrudStatus.NotExist;
                }
                else
                {
                    string _randomPassword = Utility.GetDefaultPassword(10);
                    _user.Password = Utility.getHash(_randomPassword);
                    _user.HasDefaultPassword = true;
                    _user.ModificationDate = DateTime.Now;
                    _user.ModifiedBy = WebSession.UserId;
                    _user.IsSync = false;
                    myshop.Entry(_user).State = EntityState.Modified;
                    int result = myshop.SaveChanges();
                    if (result > 0)
                    {
                        string _userFullname = _user.Firstname + " " + _user.Lastname;
                        string _emailBody = Utility.EmailUserAdminPasswordResetBody(_userFullname, _user.Username, _randomPassword);
                        Utility.EmailSendHtmlFormatted(_user.Username, "Password Reset by Admin", _emailBody);
                        return Utility.CrudStatus(result, Enums.CrudType.Update);
                    }
                    else
                    {
                        return Enums.CrudStatus.NoEffect;
                    }
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }

        public Enums.CrudStatus TaskCreate(Enums.CrudType _type, Gbl_Master_Task _model)
        {
            try
            {
                myshop = new MyshopDb();
                int _result = 0;
                if (_type == Enums.CrudType.Insert)
                {
                    Gbl_Master_Task _newTask = new Gbl_Master_Task();
                    _newTask.CreatedBy = WebSession.UserId;
                    _newTask.AssignedUserId = _model.AssignedUserId;
                    _newTask.CreatedDate = DateTime.Now;
                    _newTask.IsCompleted = false;
                    _newTask.IsDeleted = false;
                    _newTask.CompletedDate = null;
                    _newTask.ModifiedDate = DateTime.Now;
                    _newTask.IsImportant = _model.IsImportant;
                    _newTask.IsSync = false;
                    _newTask.Priority = _model.Priority;
                    _newTask.ShopId = WebSession.ShopId;
                    _newTask.TaskDetails = _model.TaskDetails;
                    myshop.Entry(_newTask).State = EntityState.Added;
                    _result = myshop.SaveChanges();
                }
                else
                {
                    var _oldTask = myshop.Gbl_Master_Task.Where(x => x.TaskId.Equals(_model.TaskId) && !x.IsDeleted && !x.IsCompleted && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                    if (_type == Enums.CrudType.Update)
                    {
                        _oldTask.TaskDetails = _model.TaskDetails;
                        _oldTask.IsSync = false;
                        _oldTask.AssignedUserId = _model.AssignedUserId;
                        _oldTask.IsCompleted = false;
                        _oldTask.CompletedDate = null;
                        _oldTask.IsImportant = _model.IsImportant;
                        _oldTask.ModifiedBy = WebSession.UserId;
                        _oldTask.ModifiedDate = DateTime.Now;
                        _oldTask.Priority = _model.Priority;
                    }
                    else if (_type == Enums.CrudType.Delete)
                    {
                        _oldTask.IsSync = false;
                        _oldTask.IsDeleted = false;
                        _oldTask.ModifiedBy = WebSession.UserId;
                        _oldTask.ModifiedDate = DateTime.Now;
                    }
                    myshop.Entry(_oldTask).State = EntityState.Modified;
                    _result = myshop.SaveChanges();
                }
                return Utility.CrudStatus(_result, _type);
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }
        public Enums.CrudStatus TaskMarkComplete(int _taskId)
        {
            try
            {
                myshop = new MyshopDb();
                var _oldTask = myshop.Gbl_Master_Task.Where(x => x.TaskId.Equals(_taskId) && x.AssignedUserId.Equals(WebSession.UserId) &&!x.IsDeleted && !x.IsCompleted && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (_oldTask != null)
                {
                    _oldTask.IsSync = false;
                    _oldTask.IsCompleted = true;
                    _oldTask.ModifiedBy = WebSession.UserId;
                    _oldTask.ModifiedDate = DateTime.Now;
                    myshop.Entry(_oldTask).State = EntityState.Modified;
                    int _result = myshop.SaveChanges();
                    if(_result>0)
                    {
                        var taskUser = myshop.Gbl_Master_Task.Where(x =>
                                                                                      x.IsDeleted == false &&
                                                                                      !x.IsCompleted &&
                                                                                      x.ShopId.Equals(WebSession.ShopId) &&
                                                                                      x.AssignedUserId.Equals(WebSession.UserId)).ToList();

                        List<TaskUserModel> _taskList = new List<TaskUserModel>();
                        foreach (Gbl_Master_Task item in taskUser)
                        {
                            TaskUserModel _newItem = new TaskUserModel();
                            _newItem.TaskId = item.TaskId;
                            _newItem.CreatedDate = item.CreatedDate;
                            _newItem.TaskCreatedByName = item.Gbl_Master_User.Firstname + " " + item.Gbl_Master_User.Lastname;
                            _newItem.TaskCreatedById = item.Gbl_Master_User.UserId;
                            _newItem.TaskCreatedByPhoto = Convert.ToBase64String(Utility.GetImageThumbnails(item.Gbl_Master_User.Photo, 30));
                            _newItem.IsImporatant = item.IsImportant;
                            _newItem.TaskAssignedUserId = item.AssignedUserId;
                            _newItem.TaskAssignedUserName = item.Gbl_Master_User1.Firstname + " " + item.Gbl_Master_User1.Lastname; ;
                            _newItem.Priority = item.Priority;
                            _newItem.TaskDetails = item.TaskDetails;
                            TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime(item.CreatedDate));
                            if (span.Days == 1)
                            {
                                _newItem.TaskAssignedTime = string.Format("{0} day ago", span.Days.ToString());
                            }
                            else if (span.Days > 1)
                            {
                                _newItem.TaskAssignedTime = string.Format("{0} days ago", span.Days.ToString());
                            }
                            else if (span.Hours >= 1 && span.Hours <= 23)
                            {
                                _newItem.TaskAssignedTime = string.Format("{0} hour ago", span.Hours.ToString());
                            }
                            else //if (span.Minutes < 60)
                            {
                                _newItem.TaskAssignedTime = string.Format("{0} min ago", span.Minutes.ToString());
                            }

                            _taskList.Add(_newItem);
                        }
                        WebSession.TaskCount = taskUser.Count();
                        WebSession.TaskList = _taskList;
                    }
                    return Utility.CrudStatus(_result, Enums.CrudType.Update);
                }
                else
                {
                    return Enums.CrudStatus.NotExist;
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }
        //public Enums.CrudStatus TaskDelete(int _taskId)
        //{
        //    try
        //    {
        //        myshop = new MyshopDb();
        //        var _oldTask = myshop.Gbl_Master_Task.Where(x => x.TaskId.Equals(_taskId) && !x.IsDeleted && !x.IsCompleted && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
        //        _oldTask.IsSync = false;
        //        _oldTask.IsDeleted = true;
        //        _oldTask.ModifiedBy = WebSession.UserId;
        //        _oldTask.ModifiedDate = DateTime.Now;
        //        myshop.Entry(_oldTask).State = EntityState.Modified;
        //        return Utility.CrudStatus(myshop.SaveChanges(), Enums.CrudType.Delete);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Enums.CrudStatus.Exception;
        //    }
        //}
        public List<TaskUserModel> TaskUserList(bool _allUserList=false,bool _addCompleteList=false)
        {
            try
            {
                myshop = new MyshopDb();
                List<TaskUserModel> _taslList = new List<TaskUserModel>();
                var _oldTaskList = myshop.Gbl_Master_Task.Where(x => !x.IsDeleted && (_addCompleteList || !x.IsCompleted) && x.ShopId.Equals(WebSession.ShopId)).ToList();
                foreach (Gbl_Master_Task _currentItem in _oldTaskList)
                {
                    TaskUserModel _newListItem = new TaskUserModel();
                    _newListItem.IsCompleted = _currentItem.IsCompleted;
                    _newListItem.CreatedDate = _currentItem.CreatedDate;
                    _newListItem.IsImporatant = _currentItem.IsImportant;
                    _newListItem.Priority = _currentItem.Priority;
                    _newListItem.TaskAssignedUserId = _currentItem.AssignedUserId;
                    _newListItem.TaskAssignedUserName = _currentItem.Gbl_Master_User1.Firstname+" "+_currentItem.Gbl_Master_User1.Lastname;
                    _newListItem.TaskCreatedById = _currentItem.CreatedBy;
                    _newListItem.TaskCreatedByName = _currentItem.Gbl_Master_User.Firstname + " " + _currentItem.Gbl_Master_User.Lastname;
                    _newListItem.TaskDetails = _currentItem.TaskDetails;
                    _newListItem.TaskId = _currentItem.TaskId;

                    if (_currentItem.Gbl_Master_User.Photo != null)
                    {
                        _newListItem.TaskCreatedByPhoto = Convert.ToBase64String(Utility.GetImageThumbnails(_currentItem.Gbl_Master_User.Photo, 30));
                    }

                    _taslList.Add(_newListItem);
                }

                return _taslList;
            }
            catch (Exception ex)
            {
                return new List<TaskUserModel>();
            }
        }


    }
}