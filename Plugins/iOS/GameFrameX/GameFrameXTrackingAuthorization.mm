//
//  GameFrameXTrackingAuthorization.mm
//  GameFrameX
//
//  Created by jiangjiayi on 2025/8/12.
//  https://github.com/gameframex
//  https://github.com/alianblank
//
//  广告跟踪授权
//
#import <Foundation/Foundation.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>
@interface GameFrameXTrackingAuthorization : NSObject

/**
 * @brief 请求广告跟踪授权
 * @details 向用户请求广告跟踪授权，并在授权结果回调中处理不同的授权状态
 */
+ (void)RequestTrackingAuthorization;

@end

NS_ASSUME_NONNULL_BEGIN

@implementation GameFrameXTrackingAuthorization

/**
 * @brief 请求广告跟踪授权
 * @details 向用户请求广告跟踪授权，并在授权结果回调中处理不同的授权状态
 */
+ (void)RequestTrackingAuthorization
{
    if (@available(iOS 14, *)) {
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
            switch (status)
                {
                case ATTrackingManagerAuthorizationStatusNotDetermined:
                    NSLog(@"跟踪授权状态: 未确定");
                    break;
                case ATTrackingManagerAuthorizationStatusRestricted:
                    NSLog(@"跟踪授权状态: 受限制");
                    break;
                case ATTrackingManagerAuthorizationStatusDenied:
                    NSLog(@"跟踪授权状态: 已拒绝");
                    break;
                case ATTrackingManagerAuthorizationStatusAuthorized:
                    NSLog(@"跟踪授权状态: 已授权");
                    break;
             }
        }];
    }
}

@end

NS_ASSUME_NONNULL_END

/**
 * @brief 打开广告跟踪授权请求
 * @details 调用请求广告跟踪授权的方法
 */
extern "C" void open_request_tracking_authorization(){
    [GameFrameXTrackingAuthorization RequestTrackingAuthorization];
}
