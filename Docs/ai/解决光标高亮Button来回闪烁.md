## 解决光标高亮Button 来回闪烁
## 询问文本
询问的 AI
``` text
为什么我的UI Button 对象
模组 选择过滤变化Sprite 
但是 鼠标浮上去时 时灵时不灵 浮上去时 Sprite 来回闪烁变化 
Button对象 有父对象， 父对象有 Vertical Layout Group 组件
为此 Button对象 直接放置在Canvas对象 下 仍然会有问题 
我的鼠标光标 替换了贴图


```

---

## 回答
👌明白了，你是必须用 UI Image 来做光标，而且要始终在 UI 之上显示，那就要解决两个问题：

1. 光标挡住 Button 导致闪烁

2. 光标要在所有 UI 元素之上

### 🔤 名词解释

1. Graphic Raycaster（图形射线投射器）

- 中文翻译：图形射线投射器

- 作用：是挂在 Canvas 上的一个组件，负责让 UI 元素（按钮、图片等）可以接收点击/悬停事件。

- 如果删除它，Canvas 里的所有 UI 元素都不会拦截鼠标事件。

2. Raycast Target（光线射线目标） **==最终解决了问题==**

- 中文翻译：射线目标（勾选框）

- 作用：在 Image、Text、RawImage 这类 UI 组件上有个勾选框。

- 如果勾选，EventSystem 的射线检测会认为这个 UI 能被点到/挡住。

- 如果取消勾选，这个 UI 还是会显示，但永远不会挡住鼠标事件。

---

### 推荐层级结构（Hierarchy）

``` mathematica
Canvas (普通UI)
 ├── Button
 ├── Panel
 └── 其他 UI

Canvas_Cursor (光标专用Canvas)
 ├── CustomCursor (空物体，挂脚本 CustomCursor)
 │    └── CursorImage (Image组件，Raycast Target 取消勾选)

```

#### 配置：

- **Canvas_Cursor**

    - Render Mode (渲染模式) = Screen Space - Overlay

    - Sorting Order (排序次序) = 9999（比普通 UI 高很多）

    - 删除 Graphic Raycaster 组件

- **CursorImage**

    - Image 组件 → Raycast Target 取消勾选

    - Sprite = 你的光标图片

2. **将鼠标位置转换为最近六边形格子坐标 (q, r)**

---

## 其他问题原因

你说的情况是：

- UI Button 设置了 Sprite State（Normal / Highlighted / Pressed）。

- 鼠标浮上去的时候，按钮的图片确实会变，但是 时灵时不灵，甚至会来回闪烁。

- 你的按钮 父对象上挂了 Vertical Layout Group。

👉 这其实是 Layout Group + Button (Image) 自动刷新 之间的冲突。

### 🔹 问题原因
1. **Vertical Layout Group** 会在运行时不断重算子物体（Button）的 RectTransform 尺寸和位置。

2. 每次 Layout Group 更新时，Unity 会强制刷新子物体的 Graphic（Image）组件。

3. 结果就是：

    - `Button` 的 `Image.sprite` 被你设为 **Highlighted Sprite**（鼠标悬浮）。

    - 但下一帧 `Layout Group` 又刷新，把它“还原”成 Normal Sprite。

    - 所以你就看到 **Sprite 来回闪烁**。

这类问题在 **Button + LayoutGroup + ContentSizeFitter** 组合下最常见。

### 🔹 解决方案

- 让按钮用一个空物体包裹：
    ```mathematica
    Button (含 Button 脚本，不带 Image)
     └── ImageObj (真正显示的图片)

    ```
- 在 Button 的 `Target Graphic(目标图形)` 里，指定子物体 `ImageObj`。

- 这样 Layout Group 只影响 Button 的 RectTransform，而不会强制刷新 Sprite。

---

### 🔹 推荐的 Button 结构

```mathematica
ButtonParent (空物体，可被 Layout Group 控制大小和位置)
│
└── Button (带 Button 脚本，但不直接挂 Image)
     │
     └── ImageObj (子物体，挂 Image 组件，用作 Target Graphic)

```
