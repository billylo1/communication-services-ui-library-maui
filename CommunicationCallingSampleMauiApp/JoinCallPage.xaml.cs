﻿#if ANDROID
using CommunicationCallingSampleMauiApp.Platforms.Android;
#elif IOS
using CommunicationCallingSampleMauiApp.Platforms.iOS;
#endif
namespace CommunicationCallingSampleMauiApp;

public partial class JoinCallPage : ContentPage
{
    IComposite callComposite = new Composite();
    bool isTeamsCall = false;

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

        groupCallFrame.IsVisible = true;
        teamsCallFrame.IsVisible = false;
        onetoNCallFrame.IsVisible = false;
        roomsCallFrame.IsVisible = false;
        callType = CallType.GroupCall;

        tokenEntry.Text = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU3Qjg2NEUwQjM0QUQ0RDQyRTM3OTRBRTAyNTAwRDVBNTE5MjA1RjUiLCJ4NXQiOiJWN2hrNExOSzFOUXVONVN1QWxBTldsR1NCZlUiLCJ0eXAiOiJKV1QifQ.eyJza3lwZWlkIjoiYWNzOjVmMGE2YTMxLWZiZWMtNDdmOC1iYTNkLWJkYzVkYzNmYmZkZV8wMDAwMDAyNS0zMGVmLTc1NDktODcwNC05MTNhMGQwMDYxYjEiLCJzY3AiOjE3OTIsImNzaSI6IjE3Mzc2MDAzODkiLCJleHAiOjE3Mzc2ODY3ODksInJnbiI6ImFtZXIiLCJhY3NTY29wZSI6InZvaXAiLCJyZXNvdXJjZUlkIjoiNWYwYTZhMzEtZmJlYy00N2Y4LWJhM2QtYmRjNWRjM2ZiZmRlIiwicmVzb3VyY2VMb2NhdGlvbiI6InVuaXRlZHN0YXRlcyIsImlhdCI6MTczNzYwMDM4OX0.lUkQjOwUJDWVpHHqeq7T3FHEY-FHJxTRXNpGSvBROKTkdIDvRkW5w_j_I2sinyaJdWZEN2BbEevwhzAZG7BO2MBI7pI5xLdhTqihY52dDuay0T6rne8F9O4h9oYPgunquFdG7nSrF5SQxn7iJ2uEnz4VGGTf7_Y_O9puyx0HsLFC6G98PUV_LhFfmkiwFsgJbiktt0EpF72kB6GO8LdsqOGuceMBscmsE5Zhy5PJXa_Fvn6o70joBsH2mEdpscnVm61rAngHkj0U9d4PBL0_XV3Nz6kZslbC_5qFA7_y8UD9W7MB1vBvSn_s0fY3AHTchw8If0msdPthN8bE4Sn2gg";
        meetingEntry.Text = "99453337661743323";
        name.Text = "Billy";
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