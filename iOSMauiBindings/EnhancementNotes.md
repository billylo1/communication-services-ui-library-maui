# Notes from the enhancements to support LocalOptions.CaptionsOptions from MAUI to ACS native SDK, and add callback to receive CaptionsData from ACS Native SDK back to MAUI

This document outlines the steps to carry out such an enhancement. It's quite complex. So, I wrote down these notes so others will have an easier time doing the same in the future.

## Getting Started

### Adjust the native side of things first (to support the callback, no changes are needed on the CaptionsOptions inbound because it's already available.)

1. Fork the ACS iOS Calling UI SDK
2. Add CallComposite Events to include a new event for callback for captions data
3. Expose CaptionsData object to the outside proxy (declare it as public)
4. Add callback flow into CaptionsViewManager (emulating what the CallStateManager does) - hooking into the eventhandler (see source code)
5. Copy the Podspec file from sdk folder to the root folder (and edit it to include the correct metadata). This is to enable the MAUI ProxyLib scripts to pull the modified ACS SDK code into MAUI
6. Note the commit label in github (for the Podfile update below)

### Communication UI Library Proxy

1. Update CommunicationUIProxy.swift file to enable sending of CaptionsOptions object into CallComposite.launch native interface.
2. Update CommunicationUIProxy.swift file to include logic to receive callback data for onCaptionsDataReceived event
3. Update Podfile to point to specific instance of ACS Calling UI SDK (:git, :branch, :commit specifics from #6 above)
4. Run iOSFramework.sh to pull the ACS SDK swift code from github, compile the CommunicationUIProxy.swift using xcodebuild to emit the Framework/DeviceFramework/SimulatorFramework files
5. Regenerate the ApiDefinitions.cs from the communication-services-ui-library-maui/iOSMauiBindings/ProxyLibs/CommunicationUI-Proxy/DeviceFramework/Release-iphoneos folder
    * sharpie bind -sdk iphoneos -output ./ -namespace iOS.CallingUI.Binding -scope ./CommunicationUI_Proxy.framework/Headers ./CommunicationUI_Proxy.framework/Headers/CommunicationUI_Proxy-Swift.h
    * If you get a System.IO.FileNotFoundException: Invalid Image error, you can ignore that error. ApiDefinitions.cs is still generated and usable.
6. Fix the errors in the ApiDefinitions.cs
    * Add using UIKit;
    * Remove using CommunicationUI_Proxy;
    * Remove Verify lines
    * Change NSURL to NSUrl
7. Copy the ApiDefinitions.cs to iOS.CallingUI.Binding 
8. Modify the Composite.cs in iOS.CallingUI.Binding to call the updated ApiDefinitions parameters (CommunicationCaptionsOptionsProxy) 
9. Add the new CaptionsOptions parameter to pass the options to the

### Lessons learned

1. Understand the flow.  It's a very long chain with lots of options object along the way.

JoinCall.xaml.cs 
-> Composite.cs (in Platforms folder)
-> Call Swift code written in CommunicationUIProxy.swift (using data structures provided by the generated ApiDefinitions.cs)
-> CommunicationUIProxy.swift then creates the CallComposite object in the ACS iOS SDK library and then launch a call (.launch())
-> The callback flow is the reversed of the above.

2. Know what needs to be compiled after you made changes to each moving part

* For .cs MAUI code, it's easy. Just build in VSCode
* For CommunicationUIProxy.swift, you need to clear the ProxyLib DeviceFramework, Framework, SimulatorFramework and run iOSFramework.sh
* For Native SDK edits, you need to compile it in Xcode, commit/push it to github, update the Podfile in ProxyLib on the MAUI to point to the right commit, rm -rf Pods DeviceFramework, Framework, SimulatorFramework and re-run iOSFramework.sh.  If you have changed the interface objects, you will also need to regenerate the ApiDefinitions.cs (and re-fix it using the steps above)
* If you are still getting unexpected results, clean the MAUI solution and rebuild the app

3. print() statements on the native side are not shown on the MAUI console (when testing with real devices). It's hard to know if the parameters are passed to the native code correctly. Luckily, if you test with iOS simulator, you can see the print() statements on VS Code console. This helps a lot in debugging data passing through the various layers.

4. CaptionOption.swift contains the class CaptionsOptions (note the missing s in the swift filename. Watch for the name accuracy. This one tripped me over.)

5. If iOSFramework.sh xcodebuild complains about usage of internal classes / structs, see if you need to make them public on the native side

