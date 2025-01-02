extern "C" void open_url(char * url) {
    NSString* url_String=  [NSString stringWithUTF8String:url];
    NSURL *URL = [NSURL URLWithString:url_String];
    [UIApplication.sharedApplication openURL:URL options:@{} completionHandler:^(BOOL success) {
        NSLog(@"Open URL %@: %d",url_String,success);
      }];
   
}

extern "C" void open_setting(){
    NSURL *url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
    if ([[UIApplication sharedApplication] canOpenURL:url]) {
        [[UIApplication sharedApplication] openURL:url options:@{} completionHandler:^(BOOL success) {
            NSLog(@"Open Setting %d",success);
        }];
    }
}
