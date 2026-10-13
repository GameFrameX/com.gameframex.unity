//
//  GameFrameXNetworkPermission.mm
//  GameFrameX
//
//  https://github.com/gameframex
//  https://github.com/alianblank
//
//  iOS 设备网络(无线数据)授权检测
//  Network (wireless data) authorization detection for China-region iOS devices
//

#import <Foundation/Foundation.h>
#import <CoreTelephony/CTCellularData.h>

/**
 * @brief 网络授权状态
 *        Network authorization status
 */
typedef NS_ENUM(NSInteger, GameFrameXNetworkPermissionStatus) {
    GameFrameXNetworkPermissionStatusUnknown = 0,       // 未知 Unknown: 无蜂窝模块设备/状态未确定 (no cellular module / undetermined)
    GameFrameXNetworkPermissionStatusRestricted = 1,    // 受限 Restricted: 未授权或用户选择了仅WLAN (not authorized or WLAN-only)
    GameFrameXNetworkPermissionStatusNotRestricted = 2  // 已授权 NotRestricted: 无线数据可用 (wireless data available)
};

@interface GameFrameXNetworkPermission : NSObject

/**
 * @brief 当前网络授权状态
 *        Current network authorization status
 * @details 0=未知 1=受限 2=已授权
 *          0=Unknown 1=Restricted 2=NotRestricted
 * @warning 受限不代表完全无网: 用户选"仅WLAN"时蜂窝为受限但WiFi可用;
 *          WiFi-only iPad/iPod 无蜂窝模块, 永远返回未知.
 *          放行判定应以真实探测请求成功为准, 本状态仅用于区分"未授权"与"网络故障"场景.
 *          Restricted does not mean completely offline: cellular is restricted while WiFi may
 *          still work if the user chose "WLAN only". WiFi-only iPad/iPod always returns Unknown.
 *          Pass-through should be decided by a real probe request; this status only helps to
 *          distinguish "not authorized" from "network failure".
 */
+ (GameFrameXNetworkPermissionStatus)status;

@end

static CTCellularData *_networkPermissionCellularData = nil;

@implementation GameFrameXNetworkPermission

/**
 * @brief 懒初始化 CTCellularData 并监听授权状态变化
 *        Lazily initialize CTCellularData and listen for authorization state changes
 * @details restrictedState 初次读取可能为未确定, 挂上 notifier 后系统会异步回调更新
 *          restrictedState may read NotDetermined at first; after the notifier is installed
 *          the system updates it asynchronously via callback
 */
+ (void)setup
{
    if (_networkPermissionCellularData != nil)
    {
        return;
    }
    _networkPermissionCellularData = [[CTCellularData alloc] init];
    _networkPermissionCellularData.cellularDataRestrictionDidUpdateNotifier = ^(CTCellularDataRestrictedState state) {
        NSLog(@"[GameFrameX] 网络授权状态变化 Network authorization state changed: %ld", (long)state);
    };
}

+ (GameFrameXNetworkPermissionStatus)status
{
    [self setup];
    switch (_networkPermissionCellularData.restrictedState)
    {
        case kCTCellularDataRestricted:
            return GameFrameXNetworkPermissionStatusRestricted;
        case kCTCellularDataNotRestricted:
            return GameFrameXNetworkPermissionStatusNotRestricted;
        default:
            return GameFrameXNetworkPermissionStatusUnknown;
    }
}

@end

/**
 * @brief 获取网络授权状态
 *        Get network authorization status
 * @details 0=未知 1=受限 2=已授权
 *          0=Unknown 1=Restricted 2=NotRestricted
 */
extern "C" int get_network_permission_status()
{
    return (int)[GameFrameXNetworkPermission status];
}
