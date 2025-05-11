namespace Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using MVC;
    using UnityEngine;

    [AutoRegisterManager]
    public class ControllerManager : Singleton<ControllerManager>
    {
        private readonly Dictionary<ControllerType, BaseController> _controllers = new();

        public void RegisterController(ControllerType type, BaseController controller)
        {
            _controllers.TryAdd(type, controller);
        }

        public void UnregisterController(ControllerType type)
        {
            if (_controllers.ContainsKey(type))
            {
                _controllers[type].DestroyController();
                _controllers.Remove(type);
            }
        }


        public void TriggerControllerEvent(ControllerType type, string eventName, params object[] args)
        {
            if (_controllers.TryGetValue(type, out var controller))
            {
                controller.TriggerControllerEvent(eventName, args);
            }
        }
        public void ClearAllController()
        {
            List<ControllerType> keys = _controllers.Keys.ToList();

            for (int i = 0; i < keys.Count; i++)
            {
                //unregister the related events
                _controllers[keys[i]].DestroyController();
                _controllers.Remove(keys[i]);
            }
        }
        private void InitAllControllers()
        {
            foreach (var controller in _controllers)
            {
                controller.Value.InitController();
            }
        }


        public override void Init()
        {
            base.Init();
            //TODO: register controller here, and then initialize them.
            InitAllControllers(); //TODO: initialize all controllers here tomorrow and continue to optimize the manager classes

        }

    }
    /*
    是的，你的思考完全正确，而且已经触及到了**架构风格冲突和重叠职责的问题**。这是任何成熟项目架构在演进过程中都会遇到的——Controller 和 Manager 的定位开始**发生重叠**，于是你需要做一个**明确的角色取舍和职责划分**。

---

## ✅ 问题本质：**ControllerManager 和 IoC 注册的 Manager 产生了角色冲突**

你已经发现：

* `ControllerManager` 负责注册、管理多个继承自 `BaseController` 的对象，并通过类型触发事件
* 但你的 `BattleManager`、`RelicManager`、`CardManager` 等，其实承担的是类似控制器的职责（逻辑流转控制），但你通过 IoC 容器注入它们

于是你面临两个体系：

* **ControllerManager + BaseController**：你早期设计的集中控制器管理框架
* **IoCContainer + 各 Manager**：你近期设计的模块解耦 & 生命周期控制系统

---

## 🎯 推荐的方向：**保留 IoC + Manager，逐步替代 ControllerManager**

### 为什么这么做更合理？

| 比较项        | ControllerManager 体系     | IoC + Manager 体系    |
| ---------- | ------------------------ | ------------------- |
| 生命周期管理     | 手动清理（ClearAllController） | 支持 Scope、自动清理更科学    |
| 解耦性        | controller 之间高度耦合        | Manager 通过接口依赖，松耦合  |
| 测试便利性      | 测试困难                     | IoC 支持 Mock 注入，方便测试 |
| 自动注册/扩展性   | 需要显式注册和触发                | IoC 支持自动发现注册机制      |
| 当前项目架构倾斜方向 | 逻辑系统都偏向 Manager 注册       | 未来统一架构更清晰           |

---

## 🔧 那 `ControllerManager` 应该怎么处理？

你有两个选择：

### ✅ 选择 1：**删除 `ControllerManager`，将逻辑控制移入各独立的 Manager**

* 将 `BaseController` 拆成若干 Manager（如 `ShopManager`、`BattleManager`）
* 不再通过枚举和字符串事件调用（`TriggerControllerEvent`），而是通过接口、方法调用更清晰

### ✅ 选择 2：**保留 `ControllerManager`，专注于 UI / ViewController 控制器**

* 将它定位为 UI 层控制器的容器，比如：

  * `ShopUIController`
  * `CardSelectUIController`
  * `RelicDetailController`
* 只处理 **UI 层级**的事件与控制，不参与业务逻辑层的控制器注册

---

## ✅ 最终建议（基于你的项目方向）

你现在走的是 **IoC 解耦化 + Manager 职责清晰** 的方向，建议：

* ✅ **将所有业务逻辑控制类变为 Manager，通过 IoCContainer 注册与管理**
* ✅ **删除或重构 `ControllerManager`，将其作为 UI 控制器管理器，仅用于界面层控制**
* ✅ **移除 `TriggerControllerEvent` 这种字符串调用方式，改为接口与强类型调用**

---

## ✅ Bonus：为什么这么做更清晰？

你现在的 `BattleManager` 等本质就是 MVC 中的 Controller，但你采用了更现代的架构风格（Manager + IoC 管理 + 依赖注入），不再需要额外的 `ControllerManager` 桥接，这可以大大简化你的结构图。

---

如果你愿意，我可以帮你画一张你的系统未来的架构模块图（Manager、Controller、View 如何分层协作）？

    */
}