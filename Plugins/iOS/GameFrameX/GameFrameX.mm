//
//  GameFrameX.mm
//
//  Created by GameframeX(AlianBlank) on 2025/8/12.
//  https://github.com/gameframex
//  https://github.com/alianblank
//

/**
 * @brief 打开指定URL链接
 * @param url 需要打开的URL字符串
 * @details 使用系统浏览器打开指定的URL链接，并在控制台输出打开结果
 */
extern "C" void open_url(char * url) {
    NSString* url_String=  [NSString stringWithUTF8String:url];
    NSURL *URL = [NSURL URLWithString:url_String];
    [UIApplication.sharedApplication openURL:URL options:@{} completionHandler:^(BOOL success) {
        NSLog(@"Open URL %@: %d",url_String,success);
      }];
   
}

/**
 * @brief 打开系统设置页面
 * @details 跳转到当前应用的系统设置页面，并在控制台输出打开结果
 */
extern "C" void open_setting(){
    NSURL *url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
    if ([[UIApplication sharedApplication] canOpenURL:url]) {
        [[UIApplication sharedApplication] openURL:url options:@{} completionHandler:^(BOOL success) {
            NSLog(@"Open Setting %d",success);
        }];
    }
}
