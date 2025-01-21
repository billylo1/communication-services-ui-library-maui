#if ANDROID
using CommunicationCallingSampleMauiApp.Platforms.Android;
#elif IOS
using CommunicationCallingSampleMauiApp.Platforms.iOS;
#endif
namespace CommunicationCallingSampleMauiApp;

public partial class JoinCallPage : ContentPage
{
    IComposite callComposite = new Composite();
#pragma warning disable IDE0044 // Add readonly modifier
    bool isTeamsCall = false;
#pragma warning restore IDE0044 // Add readonly modifier

    const String groupCallTitle = "Group call ID";
    const String groupCallEntryPlaceholder = "Enter call ID";
    const String groupCallSubtitle = "Start a call to get a call ID.";

    const String teamsMeetingTitle = "Teams meeting";
    const String teamsMeetingEntryPlaceholder = "Enter invite link";
    const String teamsMeetingSubtitle = "Get link from the meeting invite or anyone in the call.";

    const String onetoNCallTitle = "1 to N call";
    const String onetoNCallPlaceholder = "Enter comma separated MRIs";
    const String onetoNCallSubtitle = "Get CommunicationIdentifiers MRIs to dial.";

    const String roomsCallTitle = "Rooms call";
    const String roomsCallPlaceholder = "Enter room id";
    const String roomsCallSubtitle = "Get Room Id to join a rooms call";

    LocalizationProps _localization;
    DataModelInjectionProps _dataModelInjection;
    OrientationProps _orientationProps;
    CallControlProps _callControlProps;
    private CallType callType;

    public JoinCallPage()
    {
        InitializeComponent();

        _localization = new LocalizationProps();
        _localization.locale = "en";
        _localization.isLeftToRight = true;

        _orientationProps = new OrientationProps();
        _orientationProps.setupScreenOrientation = "PORTRAIT";
        _orientationProps.callScreenOrientation = "USER";

        _dataModelInjection = new DataModelInjectionProps();
        _dataModelInjection.localAvatar = "";
        _dataModelInjection.remoteAvatar = "";

        _callControlProps = new CallControlProps();
        _callControlProps.isSkipSetupON = false;
        _callControlProps.isMicrophoneON = false;
        _callControlProps.isCameraON = false;
        _callControlProps.captionsOn = true;
        _callControlProps.spokenLanguage = "en-US";

        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = false;
        roomsCallFrame.IsVisible = false;
        callType = CallType.GroupCall;

        tokenEntry.Text = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU3Qjg2NEUwQjM0QUQ0RDQyRTM3OTRBRTAyNTAwRDVBNTE5MjA1RjUiLCJ4NXQiOiJWN2hrNExOSzFOUXVONVN1QWxBTldsR1NCZlUiLCJ0eXAiOiJKV1QifQ.eyJza3lwZWlkIjoiYWNzOjVmMGE2YTMxLWZiZWMtNDdmOC1iYTNkLWJkYzVkYzNmYmZkZV8wMDAwMDAyNS0yNjZhLTE5ODAtZTYwNS05MTNhMGQwMGFhYmIiLCJzY3AiOjE3OTIsImNzaSI6IjE3Mzc0MjM4NzciLCJleHAiOjE3Mzc1MTAyNzcsInJnbiI6ImFtZXIiLCJhY3NTY29wZSI6InZvaXAiLCJyZXNvdXJjZUlkIjoiNWYwYTZhMzEtZmJlYy00N2Y4LWJhM2QtYmRjNWRjM2ZiZmRlIiwicmVzb3VyY2VMb2NhdGlvbiI6InVuaXRlZHN0YXRlcyIsImlhdCI6MTczNzQyMzg3N30.qj_rzN0iPKgUg0jabqSPxtbo9E8-LwufDp4F9RQQgHjnZrc3iy1f_2k3fAG13v_6sO3WBw11lw45J5w8KPU8AmAGsXO1O-oHsgUVjS-e2KonhhBvrMTR2vZR2P5RaIxdYYWDDZczp_JunLJ23dHvklp0Nm-SSG9-EK_OJ3O6I1ghAejod0VdV9JfVWerJySAL-OCRQT3KLkSH0xRMGjulmaz3hE6tjt9kVMQ3olVHL4BBzlWP9_xLXM9k_eSvpyauf7PzWONbveYVH_CV4502fxj6tu2d2qxJCIXrKHs1Dyt5pU_kbZYpQ_MLc61duMrByyrvNFfY09t0t4IZx_fCQ";
        meetingEntry.Text = "99439556614537041";
        name.Text = "Billy";

        OnRoomsCallClicked(this, null);

    }

    async void OnToolbarClicked(object sender, EventArgs e)
    {
        SettingsPage settingsPage = new SettingsPage(callComposite, _localization, _dataModelInjection, _orientationProps);
        settingsPage.Callback += new SettingsPage.ProcessSettingsCallback(ProcessSettings);
        await Navigation.PushModalAsync(settingsPage);
    }

    void ProcessSettings(LocalizationProps localization, DataModelInjectionProps dataModelInjection, OrientationProps orientationProps, CallControlProps callControlProps)
    {
        _localization = localization;
        _dataModelInjection = dataModelInjection;
        _orientationProps = orientationProps;
        _callControlProps = callControlProps;
        Console.WriteLine("locale is " + localization.locale + " isLeftToRight is " + localization.isLeftToRight);
    }

    void OnGroupCallClicked(object sender, EventArgs e)
    {
        callType = CallType.GroupCall;
        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = false;
        roomsCallFrame.IsVisible = false;
        teamsMeetingPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Color.FromHex("#6E6E6E");
        groupCallPivot.TextColor = Colors.White;
        roomsCallPivot.TextColor = Color.FromHex("#6E6E6E");
        meetingTitleLabel.Text = groupCallTitle;
        meetingEntry.Placeholder = groupCallEntryPlaceholder;
        meetingSubtitleLabel.Text = groupCallSubtitle;
    }

    void OnTeamsMeetingClicked(object sender, EventArgs e)
    {
        callType = CallType.TeamsCall;
        groupCallFrame.IsVisible = false;
        teamsCallFrame.IsVisible = true;
        onetoNCallFrame.IsVisible = false;
        roomsCallFrame.IsVisible = false;
        groupCallPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Color.FromHex("#6E6E6E");
        teamsMeetingPivot.TextColor = Colors.White;
        roomsCallPivot.TextColor = Color.FromHex("#6E6E6E");
        meetingTitleLabel.Text = teamsMeetingTitle;
        meetingEntry.Placeholder = teamsMeetingEntryPlaceholder;
        meetingSubtitleLabel.Text = teamsMeetingSubtitle;
    }

    void On1ToNCallClicked(object sender, EventArgs e)
    {
        callType = CallType.OneToN;
        groupCallFrame.IsVisible = false;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = true;
        roomsCallFrame.IsVisible = false;
        groupCallPivot.TextColor = Color.FromHex("#6E6E6E");
        teamsMeetingPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Colors.White;
        roomsCallPivot.TextColor = Color.FromHex("#6E6E6E");
        meetingTitleLabel.Text = onetoNCallTitle;
        meetingEntry.Placeholder = onetoNCallPlaceholder;
        meetingSubtitleLabel.Text = onetoNCallSubtitle;
    }

    void OnRoomsCallClicked(object sender, EventArgs e)
    {
        callType = CallType.RoomsCall;
        groupCallFrame.IsVisible = false;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = false;
        roomsCallFrame.IsVisible = true;
        groupCallPivot.TextColor = Color.FromHex("#6E6E6E");
        teamsMeetingPivot.TextColor = Color.FromHex("#6E6E6E");
        onetoNCallPivot.TextColor = Color.FromHex("#6E6E6E");
        roomsCallPivot.TextColor = Colors.White;
        meetingTitleLabel.Text = roomsCallTitle;
        meetingEntry.Placeholder = roomsCallPlaceholder;
        meetingSubtitleLabel.Text = roomsCallSubtitle;
    }

    void OnButtonClicked(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(tokenEntry.Text) && !String.IsNullOrEmpty(meetingEntry.Text))
        {
            callComposite.joinCall(name.Text, tokenEntry.Text, meetingEntry.Text, callType, _localization, _dataModelInjection, _orientationProps, _callControlProps);
        }
    }
}

public enum CallType
{
    TeamsCall,
    GroupCall,
    OneToN,
    RoomsCall
}