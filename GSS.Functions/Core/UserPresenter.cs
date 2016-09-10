using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSS.Functions.Interface;
using GSS.Data.Entities;
using GSS.Data.Repository;
using GSS.Models.Models;

using Notification = GSS.Models.Models.Notification;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;

namespace GSS.Functions.Core
{
    public class UserPresenter : IUserPresenter
    {
        private readonly UserRepository _objRepository = new UserRepository();

        #region User Management

        public bool AddUser(UserModel objUserInfo)
        {
            var objCandiate = new User();
            objCandiate.UserName = objUserInfo.UserName;
            objCandiate.FirstName = objUserInfo.FirstName;
            objCandiate.LastName = objUserInfo.LastName;
            objCandiate.MiddleName = objUserInfo.MiddleName;
            objCandiate.Phone = objUserInfo.Phone;
            objCandiate.EmailAddress = objUserInfo.EmailAddress;
            objCandiate.ZipCode = objUserInfo.ZipCode;
            objCandiate.Address = objUserInfo.Address;
            objCandiate.Password = objUserInfo.Password;
            objCandiate.CreatedDate = DateTime.Now;
            objCandiate.ModifiedDate = DateTime.Now;
            objCandiate.IsActive = true;
            objCandiate.Password = objUserInfo.Password;
            objCandiate.Islocked = false;

            if (objUserInfo.UserId != 0)
            {
                objCandiate.UserId = objUserInfo.UserId;
                return _objRepository.UpdateUser(objCandiate);
            }
            return _objRepository.AddUser(objCandiate);
        }

        public bool IsValid(string username, string password)
        {
            return _objRepository.Loginuser(username, password);
        }

        public bool Checkuserexist(string username)
        {
            return _objRepository.Checkuserexist(username);
        }

        public UserModel GetUser(long userid)
        {
            return _objRepository.GetUser(userid).ConvertToObject<UserModel>();
        }

        public int GetTotalCount()
        {
            return _objRepository.GetTotalCount();
        }

        public List<UserModel> GetUserList()
        {
            return _objRepository.GetUserList().ConvertToObjects<List<UserModel>>();
        }

        public bool UpdateUser(UserModel objuserinfo)
        {
            var objCandiate = new User
            {
                UserName = objuserinfo.UserName,
                FirstName = objuserinfo.FirstName,
                LastName = objuserinfo.LastName,
                MiddleName = objuserinfo.MiddleName,
                Phone = objuserinfo.Phone,
                EmailAddress = objuserinfo.EmailAddress,
                ZipCode = objuserinfo.ZipCode,
                Address = objuserinfo.Address,
                Password = objuserinfo.Password,
                CreatedDate = objuserinfo.CreatedDate,
                ModifiedDate = DateTime.Now,
                UserId = objuserinfo.UserId,
                IsActive = true,
                Islocked = objuserinfo.Islocked
            };
            return _objRepository.UpdateUser(objCandiate);
        }
        public bool Deleteuser(long id)
        {
            return _objRepository.Deleteuser(id);
        }

        public User GetUserbyusername(string userName)
        {
            return _objRepository.GetUserbyusername(userName);
        }

        public bool ChangePassword(string oldpassword, string newpassword)
        {
            return _objRepository.Changepassword(oldpassword, newpassword);
        }

        public UserModel ForgotPassword(string username)
        {
            return _objRepository.ForgotPassword(username).ConvertToObject<UserModel>();
        }

        #endregion

        #region Content Mangement

        public bool AddContent(PagesModel pagecontent)
        {
            var objpages = new Page
            {
                MetaKeywords = pagecontent.MetaKeywords,
                ModifiedDate = DateTime.Now,
                CreateDate = DateTime.Now,
                Tittle = pagecontent.Tittle,
                Description = pagecontent.Description,
                PageCotent = pagecontent.PageCotent,
                IsActive = true
            };

            if (pagecontent.Id != 0)
            {
                objpages.Id = pagecontent.Id;
                return _objRepository.UpdateContent(objpages);
            }

            return _objRepository.AddContent(objpages);
        }

        public bool UpdateContent(PagesModel pagecontent)
        {
            var objpages = new Page
            {

                MetaKeywords = pagecontent.MetaKeywords,
                ModifiedDate = DateTime.Now,
                CreateDate = DateTime.Now,
                Tittle = pagecontent.Tittle,
                Description = pagecontent.Description,
                PageCotent = pagecontent.PageCotent,
                Id = pagecontent.Id,
                IsActive = true

            };
            return _objRepository.UpdateContent(objpages);
        }

        public PagesModel GetContent(long id)
        {
            return _objRepository.GetPageContentbyId(id).ConvertToObject<PagesModel>();
        }

        public bool Deletecontent(long id)
        {
            return _objRepository.Deletecontent(id);
        }

        public IEnumerable<PagesModel> GetAllactivePages()
        {
            return _objRepository.GetallPages().ConvertToObjects<IEnumerable<PagesModel>>();
        }

        #endregion

        #region Gallery Management

        public bool DeleteImage(int id)
        {
            return _objRepository.DeleteImage(id);
        }
        public bool UpdateBannerImage(int id)
        {
            return _objRepository.UpdateBannerImage(id);
        }
        public List<GalleryModel> ImageList()
        {
            return _objRepository.GetImageList().ConvertToObjects<List<GalleryModel>>();
        }

        public List<GalleryModel> GetBannerImageList()
        {
            return _objRepository.GetBannerImageList().ConvertToObjects<List<GalleryModel>>();
        }

        public GalleryModel GetDefaultImage(long eventId)
        {
            var model = new GalleryModel();

            var repodata = _objRepository.GetDefaultImage(eventId);

            if (repodata != null)
            {
                model = repodata.ConvertToObject<GalleryModel>();
            }

            return model;
        }

        public string GeteventnamebyId(long id)
        {
            return _objRepository.GeteventnamebyId(id);

        }

        public List<GalleryModel> GetEventBasedGalleryList()
        {
            return _objRepository.GetEventBasedGalleryList().ConvertToObjects<List<GalleryModel>>();
        }


        public bool AddImages(GalleryModel gallery)
        {
            var objgallery = new Gallery
            {
                Title = gallery.Title,
                ImageName = gallery.Title,
                ImagePath = gallery.ImagePath,
                Description = gallery.Description,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActivated = true,
                IsBannerImage = gallery.IsBannerImage,
                IsDefault = gallery.IsBannerImage
            };

            if (gallery.Id != 0)
            {
                objgallery.Id = gallery.Id;
                return _objRepository.UpdateImage(objgallery);
            }
            return _objRepository.AddImages(objgallery);
        }

        public bool UpdateImage(GalleryModel gallery)
        {
            var objGallery = new Gallery
            {
                Title = gallery.Title,
                ImageName = gallery.ImageName,
                ImagePath = gallery.ImagePath,
                Description = gallery.Description,
                CreatedDate = gallery.CreateDate,
                IsBannerImage = gallery.IsBannerImage,
                ModifiedDate = DateTime.Now,
                IsActivated = true,
                Id = gallery.Id
            };

            return _objRepository.UpdateImage(objGallery);
        }

        public GalleryModel GetImage(int userid)
        {
            return _objRepository.GetImage(userid).ConvertToObject<GalleryModel>();
        }

        #endregion

        #region Contact Us Management

        public bool AddContact(ContactUsModel contact)
        {
            var objcontact = new ContactU
            {

                Address = contact.Address,
                BrachName = contact.BrachName,
                City = contact.City,
                CreateDate = DateTime.Now,
                Email = contact.Email,
                Fax = contact.Fax,
                IsActive = true,
                ModifiedDate = DateTime.Now,
                Phone = contact.Phone,
                Pincode = contact.Pincode,
                State = contact.State


            };

            if (contact.Id != 0)
            {
                objcontact.Id = contact.Id;
                return _objRepository.UpdateContact(objcontact);
            }
            return _objRepository.AddContact(objcontact);
        }

        public bool UpdateContact(ContactUsModel contact)
        {
            var objcontact = new ContactU
            {

                Address = contact.Address,
                BrachName = contact.BrachName,
                City = contact.City,
                CreateDate = DateTime.Now,
                Email = contact.Email,
                Fax = contact.Fax,
                IsActive = true,
                ModifiedDate = DateTime.Now,
                Phone = contact.Phone,
                Pincode = contact.Pincode,
                State = contact.State,
                Id = contact.Id

            };
            return _objRepository.UpdateContact(objcontact);
        }

        public bool DeleteContact(int id)
        {
            return _objRepository.Deletecontact(id);
        }

        public ContactUsModel GetContact(int id)
        {
            return _objRepository.GetContact(id).ConvertToObject<ContactUsModel>();
        }

        public IEnumerable<ContactUsModel> GetContactUsList()
        {
            return _objRepository.GetContactsList().ConvertToObjects<IEnumerable<ContactUsModel>>();
        }

        #endregion

        #region News and Events Mangement

        public bool Addnewsevent(NewsEventsModel newsEvents)
        {
            var Imagestosave = new List<GalleryModel>();
            var dataEntity = new NewsEvent();
            if (newsEvents.Id != 0)
            {
                dataEntity = _objRepository.Geteventinfo(Convert.ToInt32(newsEvents.Id));
            }
            var added = false;
            bool isupdate = false;
            var objnewsevents = new NewsEvent
            {
                CreateDate = DateTime.Now,
                Description = newsEvents.Description,
                HeadLine = newsEvents.HeadLine,
                IsActive = true,
                ModifiedDate = DateTime.Now,
                PlaceofEvent = newsEvents.PlaceofEvent,
                EventDate = newsEvents.EventDate

            };
            if (newsEvents.Id != 0)
            {
                objnewsevents.Id = newsEvents.Id;
                added = _objRepository.Updatenewsevent(objnewsevents);
                isupdate = true;
            }
            else
            {
                dataEntity = _objRepository.Addnewsevent(objnewsevents);
                if (dataEntity != null)
                {
                    added = true;
                }
            }

            #region Event Images
            var fileimages = newsEvents.ImagesBases;
            string data = string.Empty;
            var counter = 0;
            if (fileimages != null)
                foreach (var images in fileimages)
                {

                    if (images != null && (images.FileName != string.Empty))
                    {
                        if (images.FileName != string.Empty)
                        {
                            var filename = UploadDocument(images, counter);
                            if (filename != string.Empty)
                            {

                                data = SaveDocument(images, counter);

                            }


                            var image = new GalleryModel
                            {
                                ImageName = filename,
                                EventId = dataEntity.Id,
                                ImagePath = "UploadedImages/" + data,
                                IsBannerImage = false,
                                IsActivated = true,
                                CreateDate = DateTime.Now,
                                Description = dataEntity.PlaceofEvent + " " + dataEntity.HeadLine,
                                Title = dataEntity.HeadLine
                            };


                            Imagestosave.Add(image);
                        }
                    }
                    counter++;
                }

            if (Imagestosave.Any())
            {

                InsertImages(Imagestosave);
            }

            #endregion
            if (added)
            {
                var subscribers = _objRepository.GetSubscrideList().Where(a => a.IsSubscribed);

                foreach (var subscriber in subscribers)
                {
                    var name = subscriber.FirstName + " " + subscriber.LastName;

                    if (subscriber.EmailAddress != null)
                    {
                        Notification.SendEventUpdate(name, objnewsevents.HeadLine, objnewsevents.Description, name,objnewsevents.EventDate.ToString("MMM/dd/yyyy"), subscriber.EmailAddress, isupdate);
                    }

                }
            }
            return added;
        }

        public bool Updatenewsevent(NewsEventsModel newsEvents)
        {
            var objnewsevents = new NewsEvent
            {

                CreateDate = DateTime.Now,
                Description = newsEvents.Description,
                HeadLine = newsEvents.HeadLine,
                IsActive = true,
                ModifiedDate = DateTime.Now,
                Id = newsEvents.Id,
                PlaceofEvent = newsEvents.PlaceofEvent,
                EventDate = newsEvents.EventDate,

            };

            return _objRepository.Updatenewsevent(objnewsevents);
        }

        public void InsertImages(List<GalleryModel> images)
        {

            var convertimages = images.ConvertToObjects<List<Gallery>>();
            foreach (var data in convertimages)
            {
                data.CreatedDate = DateTime.Now;
                data.ModifiedDate = DateTime.Now;
            }
            _objRepository.InsertImages(convertimages);
        }
        public void InsertEventImages(List<EventImage> images)
        {
            var convertimages = images.ConvertToObjects<List<EventImage>>();
            _objRepository.InsertEventImages(convertimages);
        }

        public bool Deletenewsevent(int id)
        {
            return _objRepository.Deletenewsevents(id);
        }

        public List<NewsEventsModel> GetnewseventsList()
        {
            var data = _objRepository.GetnewseventList().ToList();
            var convert = data.ConvertToObjects<List<NewsEventsModel>>();
            foreach (var result in convert)
            {

                var getimageofevent = _objRepository.GetDefaultImage(result.Id);
                if (getimageofevent != null && getimageofevent.ImageName != null && getimageofevent.ImagePath != null)
                {
                    result.DefaultImage = new GalleryModel
                    {
                        ImageName = getimageofevent.ImageName,
                        ImagePath = getimageofevent.ImagePath,
                        EventName = result.HeadLine
                    };
                }
            }

            return convert;
        }

        public NewsEventsModel Getnewsevent(int id)
        {
            NewsEventsModel result = null;
            var dataEntity = _objRepository.Geteventinfo(id);

            if (dataEntity != null)
            {
                dataEntity.Galleries.Clear();
                result = dataEntity.ConvertToObject<NewsEventsModel>();
                result.Images = GetEventImages(result.Id).ToList();
            }
            return result;
        }

        public bool DeleteEventImage(long id, long eventId)
        {
            return _objRepository.DeleteEventImage(id, eventId);
        }

        public NewsEventsModel Geteventbydate(DateTime date)
        {
            var data = _objRepository.Geteventbydate(date);
            if (data != null)
            {
                return data.ConvertToObject<NewsEventsModel>();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<GalleryModel> GetEventImages(long eventId)
        {
            IEnumerable<GalleryModel> result = null;
            var dataEntities = _objRepository.GetEventImages(eventId).ToList();
            result = dataEntities.ConvertToObjects<IEnumerable<GalleryModel>>();
            return result;
        }

        public NewsEventsModel Geteventbycurrentdate(DateTime date)
        {
            var result = _objRepository.Geteventbycurrentdate(date);
            return result != null ? result.ConvertToObject<NewsEventsModel>() : null;
        }

        #endregion

        #region Hukamnama Management

        public bool DeleteHukamnama(long id)
        {
            return _objRepository.DeleteHukamnama(id);
        }
        public List<HukamnamaModel> GetHukamnamasList()
        {
            return _objRepository.GetHukamnamasList().ConvertToObjects<List<HukamnamaModel>>();
        }

        public bool AddHukamnama(HukamnamaModel model)
        {
            var objhukamnama = new Hukamnama
            {
                Tittle = model.Tittle,
                HukamnamImage = model.HukamnamImage,
                CratedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true
            };

            if (model.Id != 0)
            {
                objhukamnama.Id = model.Id;
                return _objRepository.UpdateHukamnama(objhukamnama);
            }
            return _objRepository.AddHukamnama(objhukamnama);
        }

        public bool UpdateHukamnama(HukamnamaModel model)
        {
            var objhukamnama = new Hukamnama
            {
                Tittle = model.Tittle,
                HukamnamImage = model.HukamnamImage,
                CratedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true
            };

            return _objRepository.UpdateHukamnama(objhukamnama);
        }

        public HukamnamaModel GetHukamnama(long userid)
        {
            return _objRepository.GetLatestHukamnama(userid).ConvertToObject<HukamnamaModel>();
        }
        public HukamnamaModel GetLatestHukamnama()
        {
            return _objRepository.GetLatestHukamnamadate().ConvertToObject<HukamnamaModel>();
        }


        #endregion

        #region CommitteeMember Section

        public bool AddCommitteeMember(CommitteMemberModel committeeMember)
        {
            var membersToCommittee = new List<MembersToCommittee>();
            bool data = false;
            if (committeeMember.Id != 0)
            {
                membersToCommittee = _objRepository.GetMembersToCommittee(committeeMember.Id);
            }

            var objcommitteeMember = new CommitteeMember
            {
                Address = committeeMember.Address,
                FirstName = committeeMember.FirstName,
                LastName = committeeMember.LastName,
                MiddleName = committeeMember.MiddleName,
                City = committeeMember.City,
                EmailAddress = committeeMember.EmailAddress,
                ContactNumber = committeeMember.ContactNumber,
                State = committeeMember.State,
                Zip = committeeMember.Zip
            };
            if (committeeMember.Id != 0)
            {
                objcommitteeMember.Id = committeeMember.Id;
                data = _objRepository.UpdateCommitteeMember(objcommitteeMember);
            }
            if (committeeMember.Id == 0)
            {
                data = _objRepository.AddCommitteeMember(objcommitteeMember);
            }

            if (membersToCommittee != null && membersToCommittee.Count > 0)
            {

                _objRepository.DeletemembersToCommittee(membersToCommittee);
                foreach (var committeeId in committeeMember.CommitteeIds)
                    _objRepository.UpdatemembersToCommittee(objcommitteeMember.Id, committeeId);
            }

            else
            {
                foreach (var committeeId in committeeMember.CommitteeIds)
                    _objRepository.InsertmembersToCommittee(objcommitteeMember.Id, committeeId);
            }

            return data;
        }

        public bool UpdateCommitteeMember(CommitteMemberModel committeeMember)
        {
            var objcommitteeMember = new CommitteeMember
            {
                Address = committeeMember.Address,
                FirstName = committeeMember.FirstName,
                LastName = committeeMember.LastName,
                MiddleName = committeeMember.MiddleName,
                City = committeeMember.City,
                EmailAddress = committeeMember.EmailAddress,
                ContactNumber = committeeMember.ContactNumber,
                State = committeeMember.State,
                Zip = committeeMember.Zip,
                Id = committeeMember.Id
            };
            return _objRepository.UpdateCommitteeMember(objcommitteeMember);
        }

        public CommitteMemberModel GetCommitteeMemberbyId(long id)
        {
            var dataEntity = _objRepository.GetCommitteeMemberbyId(id);
            var result = dataEntity.ConvertToObject<CommitteMemberModel>();
            var memberstoCommittee = dataEntity.MembersToCommittees.Where(s => s.MemberId == dataEntity.Id);
            var membersToCommittees = memberstoCommittee as IList<MembersToCommittee> ?? memberstoCommittee.ToList();
            if (!membersToCommittees.Any()) return result;
            var committeeIds = membersToCommittees.Select(membersCommittee => membersCommittee.CommitteeId).Select(committeeId => committeeId.ToString(CultureInfo.InvariantCulture)).ToList();
            result.CommitteeIds = committeeIds;
            return result;
        }

        public CommitteeModel GetCommitteeById(long id)
        {
            var dataEntity = _objRepository.GetCommitteeById(id);
            var result = dataEntity.ConvertToObject<CommitteeModel>();

            return result;
        }


        public IEnumerable<CommitteMemberModel> GetallCommitteeMembers()
        {
            var data = _objRepository.GetallCommitteeMembers();

            var converted = data.ConvertToObjects<List<CommitteMemberModel>>();

            foreach (var items in converted)
            {
                var name = GetallCommittebymemberId(items.Id).ToList();
                items.Committeename = new List<string>();
                foreach (var com in name)
                {
                    items.Committeename.Add(com.ComitteeName + " , ");
                }
            }
            return converted;
        }

        public IEnumerable<CommitteMemberModel> GetallCommitteeMembersbyCommitteeId(long id)
        {
            var data =
                _objRepository.GetAllMembersToCommittee()
                    .Where(a => a.Committee.Id == id).ToList();

            var newmemberlist = new List<CommitteMemberModel>();

            foreach (var items in data)
            {
                var member = new CommitteMemberModel();
                member = items.CommitteeMember.ConvertToObject<CommitteMemberModel>();
                member.Committeename = new List<string>();
                member.Committeename.Add(items.Committee.ComitteeName + ",");
                newmemberlist.Add(member);
            }

            return newmemberlist;
        }

        public IEnumerable<CommitteeModel> GetallCommittebymemberId(long id)
        {
            var data =
                _objRepository.GetAllMembersToCommittee()
                    .Where(a => a.CommitteeMember.Id == id).ToList();

            var newmemberlist = new List<CommitteeModel>();

            foreach (var items in data)
            {
                var member = new CommitteeModel { ComitteeName = items.Committee.ComitteeName };

                newmemberlist.Add(member);
            }

            return newmemberlist;
        }

        public IEnumerable<CustomModelCommettiee> GetAllMembersToCommittee()
        {
            var data = _objRepository.GetAllMembersToCommittee().OrderByDescending(a => a.Committee.Id);

            var filtereddata = data.GroupBy(item => item.Committee.ComitteeName,
                (key, group) => new { Customer = key, Items = group.Select(a => a.CommitteeMember).ToList() }).ToList();


            var newlist = filtereddata.Select(items => new CustomModelCommettiee
            {
                CommitteeName = items.Customer,
                Listmembers = items.Items.ConvertToObjects<List<CommitteMemberModel>>()
            }).ToList();

            return newlist;
        }


        public bool DeleteCommitteeMember(long id)
        {
            return _objRepository.DeleteCommitteeMember(id);
        }


        public IEnumerable<CommitteeModel> GetallCommittees()
        {
            return _objRepository.GetallCommittees().ConvertToObjects<List<CommitteeModel>>();
        }


        #endregion

        #region Video Gallery Management

        public bool DeleteVideo(long id)
        {
            return _objRepository.DeleteVideo(id);
        }
        public List<VideoGalleryModel> VideoList()
        {
            return _objRepository.GetVideosList().ConvertToObjects<List<VideoGalleryModel>>();
        }

        public bool AddImages(VideoGalleryModel video)
        {
            var objgallery = video.ConvertToObject<VideoGallery>();

            if (video.Id != 0)
            {
                objgallery.Id = video.Id;
                return _objRepository.UpdateVideo(objgallery);
            }
            return _objRepository.AddVideo(objgallery);
        }

        public bool UpdateImage(VideoGalleryModel video)
        {
            var objgallery = video.ConvertToObject<VideoGallery>();

            return _objRepository.UpdateVideo(objgallery);
        }

        public VideoGalleryModel GetVideo(long videoId)
        {
            return _objRepository.GetVideo(videoId).ConvertToObject<VideoGalleryModel>();
        }

        #endregion

        #region Subsription Management

        public bool DeleteSubscribed(long id)
        {
            return _objRepository.DeleteSubscription(id);
        }
        public List<SubscriptionModel> SubscribedList()
        {
            return _objRepository.GetSubscrideList().ConvertToObjects<List<SubscriptionModel>>();
        }

        public bool AddSubscriprion(SubscriptionModel subscription)
        {
            var objsubscriprion = subscription.ConvertToObject<Subscription>();

            if (subscription.Id != 0)
            {
                objsubscriprion.Id = subscription.Id;
                return _objRepository.UpdateSubscription(objsubscriprion);
            }
            return _objRepository.AddSubscription(objsubscriprion);
        }

        public bool UpdateSubscription(SubscriptionModel subscription)
        {
            var objsubscriprion = subscription.ConvertToObject<Subscription>();

            return _objRepository.UpdateSubscription(objsubscriprion);
        }

        public SubscriptionModel GetSubscriptionbyId(long id)
        {
            return _objRepository.GetSubscripedUserById(id).ConvertToObject<SubscriptionModel>();
        }

        #endregion

        #region Event gallery Management

        public bool AddneventGallery(EventGalleryModel events)
        {
            var imagestoSave = new List<EventImageModel>();
            if (events != null)
            {
                var dataEntity = _objRepository.GetGalleryEvent(events.Id);
                if (dataEntity == null)
                {
                    //this is a new Event
                    dataEntity = events.ConvertToObject<EventGallery>();
                    _objRepository.AddEventGallery(dataEntity);
                }
                else
                {
                    var update = events.ConvertToObject<EventGallery>();
                    //use this overloaded method to map the entity when making edits against an existing data element. Otherwise, it won't get saved to the database
                    _objRepository.UpdateEventGallery(update);

                }

                #region Event Images

                var fileimages = events.ImagesBases;
                string data = string.Empty;
                var counter = 0;
                if (fileimages != null)
                    foreach (var images in fileimages)
                    {

                        if (images != null && (images.FileName != string.Empty))
                        {
                            if (images.FileName != string.Empty)
                            {
                                var filename = UploadDocument(images, counter);
                                if (filename != string.Empty)
                                {

                                    data = SaveDocument(images, counter);

                                }
                                var image = new EventImageModel
                                {
                                    ImageName = filename,
                                    EventGalleryId = dataEntity.Id,
                                    ImagePath = "UploadedImages/" + data,

                                };


                                imagestoSave.Add(image);
                            }
                        }
                        counter++;
                    }

                if (imagestoSave.Any())
                {
                    var convertimagestoSave = imagestoSave.ConvertToObjects<List<EventImage>>();
                    InsertEventImages(convertimagestoSave);
                }

                #endregion

            }
            return true;
        }

        public EventGalleryModel GetGalleryEventById(long id)
        {
            EventGalleryModel result = null;
            var dataEntity = _objRepository.GetGalleryEvent(id);

            if (dataEntity != null)
            {
                dataEntity.EventImages.Clear();
                result = dataEntity.ConvertToObject<EventGalleryModel>();
                result.Images = _objRepository.GetEventGalleryImages(result.Id).ToList().ConvertToObjects<List<EventImageModel>>();
            }
            return result;
        }

        public bool UpdateventGallery(EventGalleryModel events)
        {
            var objnewsevents = new EventGallery
            {

                EventName = events.EventName,
                ThumbnailImage = events.ThumbnailImage

            };
            return _objRepository.UpdateEventGallery(objnewsevents);
        }

        public bool DeleteEventGallery(long eventId)
        {
            return _objRepository.DeleteeventGallery(eventId);
        }

        public bool DeleteEventGalleryImage(long id, long eventId)
        {
            return _objRepository.DeleteEventGalleryImage(id, eventId);
        }

        public IEnumerable<EventGalleryModel> GetAllGalleryEvnets()
        {
            IEnumerable<EventGalleryModel> result = null;
            var dataEntities = _objRepository.GetAllEvents().ToList();
            dataEntities.ForEach(a => a.EventImages.Clear());
            result = dataEntities.ConvertToObjects<IEnumerable<EventGalleryModel>>();
            return result;
        }

        public IEnumerable<EventImageModel> GetAllGalleryEvnetImagesById(long eventId)
        {
            IEnumerable<EventImageModel> result = null;
            var dataEntities = _objRepository.GetEventGalleryImages(eventId).ToList();
            result = dataEntities.ConvertToObjects<IEnumerable<EventImageModel>>();
            return result;
        }

        #endregion

        private string SaveDocument(HttpPostedFileBase fileName, int counter)
        {
            var uploadedFile = fileName;
            var str = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf('.'),
                uploadedFile.FileName.Length -
                uploadedFile.FileName.LastIndexOf('.'));
            var replace = Regex.Replace(uploadedFile.FileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            string nameoffile;
            if (replace.Length > 20)
            {
                replace = replace.Substring(0, 20);
                nameoffile = DateTime.Now.ToFileTime() + "_" + replace + counter + str;
            }
            else
            {
                nameoffile = DateTime.Now.ToFileTime() + "_" + counter + str;
            }



            string tempFolderPath = System.Web.HttpContext.Current.Server.MapPath("/UploadedImages/") + "\\" + (nameoffile);
            if (uploadedFile != null) uploadedFile.SaveAs(tempFolderPath);
            return nameoffile;
        }

        private string UploadDocument(HttpPostedFileBase filename, int counter)
        {
            string path = string.Empty;
            bool retVal = true;
            var file = filename;
            if (file != null)
            {
                string fileName = file.FileName;
                int slashCount = fileName.LastIndexOf("\\", System.StringComparison.Ordinal);
                if (slashCount > 0)
                {
                    fileName = fileName.Substring((slashCount + 1), fileName.Length - (slashCount + 1));
                }
                HttpPostedFileBase uploadedFile = file;
                string ext = string.Empty;
                if (Convert.ToInt32(uploadedFile.ContentLength.ToString()) >= 4194304)
                {
                    retVal = false;
                    string message = "Invalid upload. Please upload a file with a size not more than 4 MB!!";
                }

                else
                {
                    ext = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf('.'),
                                                                 uploadedFile.FileName.Length -
                                                                 uploadedFile.FileName.LastIndexOf('.'));
                    string[] extensions = { ".JPG", ".jpg", ".JPEG", ".jpeg", ".png", ".PNG", ".GIF", ".gif", ".tif", "TIF" };
                    if (!(extensions.Contains(ext)))
                    {
                        retVal = false;
                        string message =
                            "Invalid upload. Please upload documents only of type jpg,png  or gif!!";
                    }
                }
                if (retVal == true)
                {
                    fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                    var replace = string.Empty;
                    if (fileName.Length > 20)
                    {
                        replace = fileName.Substring(0, 20);
                    }
                    fileName = DateTime.Now.ToFileTime() + "_" + counter + replace + ext;
                    path = fileName;
                }
            }

            return path;
        }
    }

}
