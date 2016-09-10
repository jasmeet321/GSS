using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSS.Data.Entities;
using GSS.Data;

namespace GSS.Data.Repository
{
    public class UserRepository
    {
        private readonly GSSEntities _entity = new GSSEntities();

        //User Management

        #region User Section

        public bool Checkuserexist(string username)
        {

            var count = _entity.Users.Count(d => d.UserName == username);

            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public User ForgotPassword(string username)
        {
            var user = _entity.Users.First(a => a.UserName == username);

            if (user != null)
            {
                user.Password = GenerateNewPassword();
                _entity.SaveChanges();
                return user;
            }

            else
            {
                return null;
            }
        }

        private string GenerateNewPassword()
        {
            const string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#@$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            var rnd = new Random();
            for (int i = 0; i <= 8; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }

        public bool AddUser(User userInfo)
        {
            try
            {
                var count = _entity.Users.Count(d => d.UserName == userInfo.UserName);
                if (count == 0)
                {
                    _entity.Users.Add(userInfo);
                    _entity.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Loginuser(string username, string passsword)
        {
            try
            {
                bool userValid = _entity.Users.Any(user => user.UserName == username && user.Password == passsword && user.IsActive && !user.Islocked);
                return userValid;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetUser(long userId)
        {
            var user = _entity.Users.First(a => a.UserId == userId);
            return user;
        }

        public User GetUserbyusername(string userName)
        {
            return _entity.Users.OfType<User>().FirstOrDefault(n => n.UserName == userName);
        }

        public bool UpdateUser(User userInfo)
        {
            try
            {
                var objuser = _entity.Users.Single(d => d.UserId == userInfo.UserId);
                objuser.IsActive = true;
                objuser.UserName = userInfo.UserName;
                objuser.FirstName = userInfo.FirstName;
                objuser.LastName = userInfo.LastName;
                objuser.MiddleName = userInfo.MiddleName;
                objuser.Phone = userInfo.Phone;
                objuser.EmailAddress = userInfo.EmailAddress;
                objuser.ZipCode = userInfo.ZipCode;
                objuser.Islocked = false;
                objuser.Password = userInfo.Password;
                objuser.Address = userInfo.Address;
                objuser.ModifiedDate = DateTime.Now;
                objuser.CreatedDate = DateTime.Now;
                _entity.SaveChanges();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }


        public List<User> GetUserList()
        {
            var userList = _entity.Users.Where(a => a.IsActive == true).ToList();
            return userList;
        }

        public int GetTotalCount()
        {
            return _entity.Users.Count();
        }

        public bool Deleteuser(long id)
        {
            try
            {
                User user = _entity.Users.First(m => m.UserId == id);
                _entity.Users.Remove(user);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }


        public bool Changepassword(string oldpassword, string newpassword)
        {
            var user = _entity.Users.First(a => a.Password == oldpassword);

            if (user != null)
            {
                user.Password = newpassword;
                _entity.SaveChanges();
                return true;
            }

            else
            {
                return false;
            }

        }

        #endregion


        // Page Management

        #region Content/Pages Section

        public bool AddContent(Page pagecontent)
        {

            try
            {
                _entity.Pages.Add(pagecontent);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateContent(Page pagecontent)
        {
            try
            {
                var objpages = _entity.Pages.Single(d => d.Id == pagecontent.Id);
                objpages.IsActive = true;
                objpages.MetaKeywords = pagecontent.MetaKeywords;
                objpages.ModifiedDate = DateTime.Now;
                objpages.CreateDate = DateTime.Now;
                objpages.Tittle = pagecontent.Tittle;
                objpages.Description = pagecontent.Description;
                objpages.PageCotent = pagecontent.PageCotent;
                _entity.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public Page GetPageContentbyId(long id)
        {
            var content = _entity.Pages.First(a => a.Id == id);
            return content;
        }

        public IEnumerable<Page> GetallPages()
        {
            var pagesList = _entity.Pages.Where(a => a.IsActive == true).ToList();
            return pagesList;
        }

        public bool Deletecontent(long id)
        {
            try
            {
                Page page = _entity.Pages.First(m => m.Id == id);
                _entity.Pages.Remove(page);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }

        #endregion

        // Gallery Mangement

        #region Gallery Section

        public List<Gallery> GetImageList()
        {

            var imagelist = _entity.Galleries.Where(a => a.IsActivated == true && a.EventId == null && a.IsDefault.Value == true).ToList();
            return imagelist;
        }

        public Gallery GetImage(int userId)
        {
            var gallery = _entity.Galleries.First(a => a.Id == userId);
            return gallery;
        }

        public Gallery GeteventImage(int id, long eventId)
        {
            var gallery = _entity.Galleries.First(a => a.Id == id && a.EventId == eventId);
            return gallery;
        }

        public string GeteventnamebyId(long id)
        {

            var eventname = _entity.NewsEvents.Where(a => a.Id == id).FirstOrDefault().HeadLine;

            return eventname;
        }

        public List<Gallery> GetBannerImageList()
        {
            var bannerImagelist = _entity.Galleries.Where(a => a.IsActivated == true && a.IsBannerImage.Value == true).ToList();
            return bannerImagelist;
        }

        public Gallery GetDefaultImage(long eventId)
        {
            var defaultimage = _entity.Galleries.Where(a => a.IsActivated == true && a.IsBannerImage.Value == false && a.EventId != null && a.EventId == eventId).FirstOrDefault();
            return defaultimage;
        }

        public List<Gallery> GetEventBasedGalleryList()
        {
            var imagelist = _entity.Galleries.Where(a => a.IsActivated == true && a.IsBannerImage.Value == false && a.EventId != null).ToList();
            return imagelist;
        }

        public bool AddImages(Gallery galleryinfo)
        {
            try
            {
                _entity.Galleries.Add(galleryinfo);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateImage(Gallery galleryinfo)
        {
            try
            {

                var objgallery = _entity.Galleries.Single(d => d.Id == galleryinfo.Id);
                objgallery.ImageName = galleryinfo.ImageName;
                objgallery.Title = galleryinfo.Title;
                objgallery.ImagePath = galleryinfo.ImagePath;
                objgallery.IsActivated = true;
                objgallery.IsBannerImage = galleryinfo.IsBannerImage;
                objgallery.Description = galleryinfo.Description;
                objgallery.CreatedDate = DateTime.Now;
                objgallery.ModifiedDate = DateTime.Now;

                _entity.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEventImage(long id, long eventId)
        {
            var image = GeteventImage(Convert.ToInt32(id), eventId);
            _entity.Galleries.Remove(image);
            _entity.SaveChanges();
            return true;
        }

        public bool DeleteImage(int id)
        {
            try
            {
                Gallery gallery = _entity.Galleries.First(m => m.Id == id);
                _entity.Galleries.Remove(gallery);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }


        public bool UpdateBannerImage(int id)
        {
            try
            {
                Gallery gallery = _entity.Galleries.First(m => m.Id == id);
                if (gallery != null)
                {
                    if (gallery.IsBannerImage.Value == true)
                    {
                        gallery.IsBannerImage = false;
                    }
                    else
                    {
                        gallery.IsBannerImage = true;
                    }
                }
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }

        #endregion


        //Contact Us  Mangement

        #region Contact us Section

        public bool AddContact(ContactU contactInfo)
        {
            try
            {
                var count = _entity.ContactUs.Count(d => d.BrachName == contactInfo.BrachName && d.Address == contactInfo.Address);
                if (count == 0)
                {
                    _entity.ContactUs.Add(contactInfo);
                    _entity.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateContact(ContactU contactinfo)
        {
            try
            {

                var objcontact = _entity.ContactUs.Single(d => d.Id == contactinfo.Id);
                objcontact.Address = contactinfo.Address;
                objcontact.BrachName = contactinfo.BrachName;
                objcontact.City = contactinfo.City;
                objcontact.CreateDate = DateTime.Now;
                objcontact.Fax = contactinfo.Fax;
                objcontact.Phone = contactinfo.Phone;
                objcontact.Pincode = contactinfo.Pincode;
                objcontact.State = contactinfo.State;
                objcontact.ModifiedDate = DateTime.Now;
                objcontact.Email = contactinfo.Email;
                objcontact.IsActive = true;
                _entity.SaveChanges();
                return true;

            }


            catch (Exception)
            {
                return false;
            }
        }

        public ContactU GetContact(int id)
        {

            var contact = _entity.ContactUs.First(a => a.Id == id);
            return contact;

        }

        public bool Deletecontact(int id)
        {
            try
            {
                ContactU contact = _entity.ContactUs.First(m => m.Id == id);
                _entity.ContactUs.Remove(contact);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }
        public IEnumerable<ContactU> GetContactsList()
        {
            var contactlist = _entity.ContactUs.Where(a => a.IsActive == true).ToList();
            return contactlist;
        }
        #endregion

        //Event Mangement

        #region NewsEvents Section


        public NewsEvent Addnewsevent(NewsEvent eventinfo)
        {
            try
            {

                _entity.NewsEvents.Add(eventinfo);
                _entity.SaveChanges();
                return eventinfo;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public void InsertImages(List<Gallery> images)
        {

            if (images == null) return;
            foreach (var imagestosave in images.Distinct())
            {
                _entity.Galleries.Add(imagestosave);
                _entity.SaveChanges();
            }
        }

        public bool Updatenewsevent(NewsEvent eventinfo)
        {
            try
            {

                var objnewsevent = _entity.NewsEvents.Single(d => d.Id == eventinfo.Id);

                objnewsevent.CreateDate = DateTime.Now;
                objnewsevent.Description = eventinfo.Description;
                objnewsevent.HeadLine = eventinfo.HeadLine;
                objnewsevent.IsActive = true;
                objnewsevent.EventDate = eventinfo.EventDate;
                objnewsevent.PlaceofEvent = eventinfo.PlaceofEvent;
                objnewsevent.ModifiedDate = DateTime.Now;
                if (eventinfo.Galleries.Where(a => a.EventId != null).Any())
                {
                    objnewsevent.Galleries = eventinfo.Galleries;
                }
                else
                {
                    var images = _entity.Galleries.Where(a => a.EventId == eventinfo.Id);
                    foreach (var result in images)
                    {
                        if (result.EventId == null)
                        {
                            result.EventId = eventinfo.Id;
                        }
                    }
                    objnewsevent.Galleries = images.ToList();

                }
                _entity.SaveChanges();
                return true;

            }


            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Gallery> GetEventImages(long eventId)
        {
            var query = _entity.Galleries.Where(a => a.EventId == eventId && a.IsActivated && a.IsBannerImage != true);
            //To Do :: after implemetation of search functionality we will pass one more param as searchstring than we will remove this statc value//
            return query.OrderByDescending(a => a.Id).ToList();
        }


        public NewsEvent Geteventinfo(int id)
        {

            var newsevent = _entity.NewsEvents.First(a => a.Id == id);
            return newsevent;

        }
        public NewsEvent Geteventbydate(DateTime date)
        {

            var existcurrentdate = _entity.NewsEvents.FirstOrDefault(a => a.EventDate == date);
            return existcurrentdate ?? _entity.NewsEvents.OrderBy(a => a.EventDate).Take(1).FirstOrDefault();
        }

        public NewsEvent Geteventbycurrentdate(DateTime date)
        {
            return _entity.NewsEvents.FirstOrDefault(a => a.EventDate == date);
        }

        public bool Deletenewsevents(int id)
        {
            try
            {
                NewsEvent newsevent = _entity.NewsEvents.First(m => m.Id == id);

                var geteventImages = _entity.Galleries.Where(a => a.EventId == id).ToList();
                if (geteventImages != null && geteventImages.Any())
                {
                    _entity.Galleries.RemoveRange(geteventImages);
                }
                _entity.NewsEvents.Remove(newsevent);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }

        }

        public List<NewsEvent> GetnewseventList()
        {
            var newseventlist = _entity.NewsEvents.Where(a => a.IsActive == true).OrderBy(a => a.EventDate).ToList();
            return newseventlist;
        }


        #endregion

        // Hukamnama Section

        #region Hukamnama Section

        public List<Hukamnama> GetHukamnamasList()
        {

            var list = _entity.Hukamnamas.Where(a => a.IsActive == true).ToList();
            return list;
        }

        public Hukamnama GetLatestHukamnama(long id)
        {
            var hukamnama = _entity.Hukamnamas.First(a => a.Id == id);
            return hukamnama;
        }

        public Hukamnama GetLatestHukamnamadate()
        {
            var hukamnama = _entity.Hukamnamas.OrderByDescending(a => a.CratedDate).FirstOrDefault();
            return hukamnama;
        }


        public bool AddHukamnama(Hukamnama hukamnama)
        {
            try
            {
                _entity.Hukamnamas.Add(hukamnama);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateHukamnama(Hukamnama hukamnamainfo)
        {
            try
            {

                var objhukamnama = _entity.Hukamnamas.Single(d => d.Id == hukamnamainfo.Id);
                objhukamnama.HukamnamImage = hukamnamainfo.HukamnamImage;
                objhukamnama.Tittle = hukamnamainfo.Tittle;
                objhukamnama.IsActive = true;
                objhukamnama.CratedDate = DateTime.Now;
                objhukamnama.ModifiedDate = DateTime.Now;

                _entity.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteHukamnama(long id)
        {
            try
            {
                Hukamnama hukamanma = _entity.Hukamnamas.First(m => m.Id == id);
                _entity.Hukamnamas.Remove(hukamanma);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }

        #endregion


        // Committee Member Section

        #region Committee Member Section

        public bool AddCommitteeMember(CommitteeMember committeeMember)
        {

            try
            {
                _entity.CommitteeMembers.Add(committeeMember);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateCommitteeMember(CommitteeMember committeeMember)
        {
            try
            {
                var objmembers = _entity.CommitteeMembers.Single(d => d.Id == committeeMember.Id);
                objmembers.FirstName = committeeMember.FirstName;
                objmembers.LastName = committeeMember.LastName;
                objmembers.MiddleName = committeeMember.MiddleName;
                objmembers.Address = committeeMember.Address;
                objmembers.EmailAddress = committeeMember.EmailAddress;
                objmembers.ContactNumber = committeeMember.ContactNumber;
                objmembers.City = committeeMember.City;
                objmembers.State = committeeMember.State;
                objmembers.Zip = committeeMember.Zip;
                objmembers.Id = committeeMember.Id;
                _entity.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<MembersToCommittee> GetMembersToCommittee(long? id)
        {
            var result = _entity.MembersToCommittees.Where(sr => sr.MemberId == id).ToList();
            return result;
        }

        public IEnumerable<MembersToCommittee> GetAllMembersToCommittee()
        {
            var result = _entity.MembersToCommittees.ToList();
            return result;
        }


        public void UpdatemembersToCommittee(long memberId, string committeId)
        {
            var membersToCommittee = new MembersToCommittee
            {
                CommitteeId = long.Parse(committeId),
                MemberId = memberId
            };

            _entity.MembersToCommittees.Add(membersToCommittee);
            _entity.SaveChanges();
        }

        public void InsertmembersToCommittee(long memberId, string committeeId)
        {

            var membersToCommittee = new MembersToCommittee
            {
                CommitteeId = long.Parse(committeeId),
                MemberId = memberId
            };

            _entity.MembersToCommittees.Add(membersToCommittee);
            _entity.SaveChanges();
        }


        public void DeletemembersToCommittee(List<MembersToCommittee> membersToCommittee)
        {
            foreach (var membersincommittee in membersToCommittee)
            {
                var existing = _entity.MembersToCommittees.Remove(membersincommittee);
            }
        }


        public CommitteeMember GetCommitteeMemberbyId(long id)
        {
            var member = _entity.CommitteeMembers.First(a => a.Id == id);
            return member;
        }

        public Committee GetCommitteeById(long id)
        {
            var committee = _entity.Committees.First(a => a.Id == id);
            return committee;
        }

        public IEnumerable<Committee> GetallCommittees()
        {
            var committeeList = _entity.Committees.ToList();
            return committeeList;
        }

        public IEnumerable<CommitteeMember> GetallCommitteeMembers()
        {
            var membersList = _entity.CommitteeMembers.ToList();
            return membersList;
        }

        public bool DeleteCommitteeMember(long id)
        {
            try
            {
                var member = _entity.CommitteeMembers.First(m => m.Id == id);
                _entity.CommitteeMembers.Remove(member);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }
        }

        #endregion

        //Video Gallery Section

        #region Gallery Section

        public List<VideoGallery> GetVideosList()
        {

            var videolist = _entity.VideoGalleries.ToList();
            return videolist;
        }

        public VideoGallery GetVideo(long videoId)
        {
            var video = _entity.VideoGalleries.First(a => a.Id == videoId);
            return video;
        }


        public bool AddVideo(VideoGallery video)
        {
            try
            {
                _entity.VideoGalleries.Add(video);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateVideo(VideoGallery videoeoinfo)
        {
            try
            {

                var objgallery = _entity.VideoGalleries.Single(d => d.Id == videoeoinfo.Id);
                objgallery.VideoURL = videoeoinfo.VideoURL;
                objgallery.Tittle = videoeoinfo.Tittle;
                objgallery.Description = videoeoinfo.Description;
                objgallery.Id = videoeoinfo.Id;
                _entity.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteVideo(long id)
        {
            try
            {
                VideoGallery video = _entity.VideoGalleries.First(m => m.Id == id);
                _entity.VideoGalleries.Remove(video);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }

        #endregion


        // Subscription Section

        #region Subscription Section

        public List<Subscription> GetSubscrideList()
        {

            var subscribedList = _entity.Subscriptions.ToList();
            return subscribedList;
        }

        public Subscription GetSubscripedUserById(long id)
        {
            var subscription = _entity.Subscriptions.First(a => a.Id == id);
            return subscription;
        }


        public bool AddSubscription(Subscription subscription)
        {
            try
            {
                _entity.Subscriptions.Add(subscription);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSubscription(Subscription subscription)
        {
            try
            {

                var objsubscription = _entity.Subscriptions.Single(d => d.Id == subscription.Id);
                objsubscription.Address = subscription.Address;
                objsubscription.ContactNumber = subscription.ContactNumber;
                objsubscription.EmailAddress = subscription.EmailAddress;
                objsubscription.Id = subscription.Id;
                objsubscription.FirstName = subscription.FirstName;
                objsubscription.LastName = subscription.LastName;
                objsubscription.IsSubscribed = subscription.IsSubscribed;
                _entity.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSubscription(long id)
        {
            try
            {
                Subscription subscription = _entity.Subscriptions.First(m => m.Id == id);
                _entity.Subscriptions.Remove(subscription);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }

        }

        #endregion

        //Event Specific Gallery Section


        #region Event Specific Gallery Section


        public bool AddEventGallery(EventGallery eventinfo)
        {
            try
            {
                _entity.EventGalleries.Add(eventinfo);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void InsertEventImages(List<EventImage> images)
        {

            if (images == null) return;
            foreach (var imagestosave in images.Distinct())
            {
                _entity.EventImages.Add(imagestosave);
                _entity.SaveChanges();
            }
        }

        public EventGallery GetGalleryEvent(long id)
        {
            var gallery = _entity.EventGalleries.FirstOrDefault(a => a.Id == id);
            return gallery;
        }

        public bool UpdateEventGallery(EventGallery eventinfo)
        {
            try
            {

                var objnewsevent = _entity.EventGalleries.Single(d => d.Id == eventinfo.Id);


                objnewsevent.EventName = eventinfo.EventName;
                objnewsevent.ThumbnailImage = eventinfo.ThumbnailImage;
                objnewsevent.Id = eventinfo.Id;
                if (eventinfo.EventImages.Any())
                {
                    objnewsevent.EventImages = eventinfo.EventImages;
                }
                else
                {
                    var images = _entity.EventImages.Where(a => a.EventGalleryId == eventinfo.Id);
                    foreach (var result in images)
                    {
                        if (result.EventGalleryId == null)
                        {
                            result.EventGalleryId = eventinfo.Id;
                        }
                    }
                    objnewsevent.EventImages = images.ToList();

                }
                _entity.SaveChanges();
                return true;

            }


            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<EventImage> GetEventGalleryImages(long eventGalleryId)
        {
            var query = _entity.EventImages.Where(a => a.EventGalleryId == eventGalleryId);
            //To Do :: after implemetation of search functionality we will pass one more param as searchstring than we will remove this statc value//
            return query.OrderByDescending(a => a.Id).ToList();
        }


        public IEnumerable<EventGallery> GetAllEvents()
        {
            var query = _entity.EventGalleries;

            //To Do :: after implemetation of search functionality we will pass one more param as searchstring than we will remove this statc value//
            return query.OrderByDescending(a => a.Id).ToList();
        }

        public bool DeleteeventGallery(long id)
        {
            try
            {
                EventGallery eventGallery = _entity.EventGalleries.First(m => m.Id == id);
                var geteventgalleryImages = _entity.EventImages.Where(a => a.EventGalleryId == id).ToList();
                if (geteventgalleryImages != null && geteventgalleryImages.Any())
                {
                    _entity.EventImages.RemoveRange(geteventgalleryImages);
                }
                _entity.EventGalleries.Remove(eventGallery);
                _entity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Logging.TrackError(ex, "CreateLab");
                return false;
            }
        }

        public EventImage GeteventGalleryImage(long id, long eventId)
        {
            var gallery = _entity.EventImages.First(a => a.Id == id && a.EventGalleryId == eventId);
            return gallery;
        }

        public bool DeleteEventGalleryImage(long id, long eventId)
        {
            var image = GeteventGalleryImage(id, eventId);
            _entity.EventImages.Remove(image);
            _entity.SaveChanges();
            return true;
        }

        #endregion
    }
}
