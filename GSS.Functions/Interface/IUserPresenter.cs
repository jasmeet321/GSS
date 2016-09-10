using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSS.Data.Entities;
using GSS.Models.Models;


namespace GSS.Functions.Interface
{
    public interface IUserPresenter
    {
        bool AddUser(UserModel objUserInfo);
        bool IsValid(string username, string password);
        UserModel GetUser(long userid);
        List<GalleryModel> GetEventBasedGalleryList();
        int GetTotalCount();
        string GeteventnamebyId(long id);
        List<UserModel> GetUserList();
        List<GalleryModel> ImageList();
        List<GalleryModel> GetBannerImageList();
        GalleryModel GetDefaultImage(long eventId);
        IEnumerable<GalleryModel> GetEventImages(long eventId);
        bool DeleteEventImage(long id, long eventId);
        bool UpdateUser(UserModel objuserinfo);
        bool AddImages(GalleryModel gallery);
        bool UpdateImage(GalleryModel gallery);
        bool DeleteImage(int id);
        bool UpdateBannerImage(int id);
        bool Deleteuser(long id);
        GalleryModel GetImage(int userid);
        User GetUserbyusername(string userName);
        bool ChangePassword(string oldpassword, string newpassword);
        UserModel ForgotPassword(string username);
        bool AddContent(PagesModel content);
        bool UpdateContent(PagesModel content);
        PagesModel GetContent(long id);
        bool Deletecontent(long id);
        IEnumerable<PagesModel> GetAllactivePages();
        bool Checkuserexist(string username);
        bool AddContact(ContactUsModel contact);
        bool UpdateContact(ContactUsModel contact);
        bool DeleteContact(int id);
        IEnumerable<ContactUsModel> GetContactUsList();
        ContactUsModel GetContact(int id);
        bool Addnewsevent(NewsEventsModel contact);
        bool Updatenewsevent(NewsEventsModel contact);
        bool Deletenewsevent(int id);
        List<NewsEventsModel> GetnewseventsList();
        NewsEventsModel Geteventbydate(DateTime date);
        NewsEventsModel Geteventbycurrentdate(DateTime date);
        NewsEventsModel Getnewsevent(int id);
        List<HukamnamaModel> GetHukamnamasList();
        bool UpdateHukamnama(HukamnamaModel objuserinfo);
        bool AddHukamnama(HukamnamaModel gallery);
        bool DeleteHukamnama(long id);
        HukamnamaModel GetHukamnama(long id);
        HukamnamaModel GetLatestHukamnama();
        bool AddCommitteeMember(CommitteMemberModel committeeMember);
        bool UpdateCommitteeMember(CommitteMemberModel committeeMember);
        CommitteMemberModel GetCommitteeMemberbyId(long id);
        IEnumerable<CommitteMemberModel> GetallCommitteeMembers();
        IEnumerable<CustomModelCommettiee> GetAllMembersToCommittee();
        bool DeleteCommitteeMember(long id);
        IEnumerable<CommitteeModel> GetallCommittees();
        CommitteeModel GetCommitteeById(long id);
        bool DeleteVideo(long id);
        List<VideoGalleryModel> VideoList();
        bool AddImages(VideoGalleryModel video);
        bool UpdateImage(VideoGalleryModel video);
        VideoGalleryModel GetVideo(long videoId);
        bool DeleteSubscribed(long id);
        List<SubscriptionModel> SubscribedList();
        bool AddSubscriprion(SubscriptionModel subscription);
        bool UpdateSubscription(SubscriptionModel subscription);
        SubscriptionModel GetSubscriptionbyId(long id);
        IEnumerable<CommitteMemberModel> GetallCommitteeMembersbyCommitteeId(long id);
        IEnumerable<CommitteeModel> GetallCommittebymemberId(long id);
        bool AddneventGallery(EventGalleryModel events);
        EventGalleryModel GetGalleryEventById(long id);
        bool UpdateventGallery(EventGalleryModel events);
        bool DeleteEventGallery(long eventId);
        bool DeleteEventGalleryImage(long id, long eventId);
        IEnumerable<EventGalleryModel> GetAllGalleryEvnets();
        IEnumerable<EventImageModel> GetAllGalleryEvnetImagesById(long eventId);
    }
}
