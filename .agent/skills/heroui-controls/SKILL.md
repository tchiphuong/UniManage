---
name: heroui-controls
description: Hero UI Control components (Switch, Slider, Toggle). Use for settings, filters, volume controls in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Control Components

Complete guide for Hero UI Control components. For detailed examples, visit the official documentation links below.

## Switch Component

**Docs**: https://v3.heroui.com/docs/components/switch  
**Import**: `import { Switch } from '@heroui/react';`

Toggle control for binary on/off settings.

### Anatomy

```tsx
<Switch>
    <Switch.Control>
        <Switch.Thumb />
        <Switch.Icon />
    </Switch.Control>
    <Label>Enable notifications</Label>
</Switch>
```

### Key Props (Switch)

-   `isSelected`: `boolean` - Controlled selected state
-   `defaultSelected`: `boolean` - Default selected (uncontrolled)
-   `onChange`: `(isSelected: boolean) => void` - Change handler
-   `isDisabled`: `boolean` - Disable switch
-   `size`: `'sm' | 'md' | 'lg'` (default: `'md'`)

### Basic Usage

```tsx
import { Switch, Label } from "@heroui/react";

function SwitchExample() {
    return (
        <Switch>
            <Switch.Control>
                <Switch.Thumb />
            </Switch.Control>
            <Label>Enable notifications</Label>
        </Switch>
    );
}
```

### Controlled Switch

```tsx
import { Switch, Label } from "@heroui/react";
import { useState } from "react";

function ControlledSwitch() {
    const [enabled, setEnabled] = useState(false);

    return (
        <Switch isSelected={enabled} onChange={setEnabled}>
            <Switch.Control>
                <Switch.Thumb />
            </Switch.Control>
            <Label>{enabled ? "On" : "Off"}</Label>
        </Switch>
    );
}
```

### With Icon

```tsx
import { Switch, Label } from "@heroui/react";
import { CheckIcon, XIcon } from "lucide-react";

<Switch>
    <Switch.Control>
        <Switch.Thumb />
        <Switch.Icon>{(state) => (state.isSelected ? <CheckIcon /> : <XIcon />)}</Switch.Icon>
    </Switch.Control>
    <Label>Auto-save</Label>
</Switch>;
```

### Size Variants

```tsx
// Small
<Switch size="sm">
  <Switch.Control><Switch.Thumb /></Switch.Control>
  <Label>Small</Label>
</Switch>

// Medium (default)
<Switch size="md">
  <Switch.Control><Switch.Thumb /></Switch.Control>
  <Label>Medium</Label>
</Switch>

// Large
<Switch size="lg">
  <Switch.Control><Switch.Thumb /></Switch.Control>
  <Label>Large</Label>
</Switch>
```

### With Description

```tsx
import { Switch, Label, Description } from "@heroui/react";

<div className="flex flex-col gap-1">
    <Switch>
        <Switch.Control>
            <Switch.Thumb />
        </Switch.Control>
        <div>
            <Label>Email notifications</Label>
            <Description>Receive email updates about your account</Description>
        </div>
    </Switch>
</div>;
```

### Label Position

```tsx
// Label after control (default)
<Switch>
  <Switch.Control><Switch.Thumb /></Switch.Control>
  <Label>Setting</Label>
</Switch>

// Label before control
<Switch>
  <Label>Setting</Label>
  <Switch.Control><Switch.Thumb /></Switch.Control>
</Switch>
```

---

## SwitchGroup Component

**Docs**: https://v3.heroui.com/docs/components/switch-group  
**Import**: `import { SwitchGroup } from '@heroui/react';`

Group multiple switches with shared layout.

### Usage

```tsx
import { SwitchGroup, Switch, Label } from "@heroui/react";

<SwitchGroup>
    <Switch>
        <Switch.Control>
            <Switch.Thumb />
        </Switch.Control>
        <Label>Email notifications</Label>
    </Switch>

    <Switch>
        <Switch.Control>
            <Switch.Thumb />
        </Switch.Control>
        <Label>Push notifications</Label>
    </Switch>

    <Switch>
        <Switch.Control>
            <Switch.Thumb />
        </Switch.Control>
        <Label>SMS notifications</Label>
    </Switch>
</SwitchGroup>;
```

### Orientation

```tsx
// Vertical (default)
<SwitchGroup orientation="vertical">
  {/* switches */}
</SwitchGroup>

// Horizontal
<SwitchGroup orientation="horizontal">
  {/* switches */}
</SwitchGroup>
```

---

## Slider Component

**Docs**: https://v3.heroui.com/docs/components/slider  
**Import**: `import { Slider } from '@heroui/react';`

Select value(s) from a range.

### Anatomy

```tsx
<Slider>
    <Slider.Label>Volume</Slider.Label>
    <Slider.Track>
        <Slider.Range />
        <Slider.Thumb />
    </Slider.Track>
    <Slider.ValueLabel />
</Slider>
```

### Key Props (Slider)

-   `value`: `number | number[]` - Controlled value(s)
-   `defaultValue`: `number | number[]` - Default value (uncontrolled)
-   `onChange`: `(value: number | number[]) => void` - Change handler
-   `min`: `number` (default: `0`) - Minimum value
-   `max`: `number` (default: `100`) - Maximum value
-   `step`: `number` (default: `1`) - Step increment
-   `isDisabled`: `boolean` - Disable slider
-   `orientation`: `'horizontal' | 'vertical'` (default: `'horizontal'`)

### Basic Usage

```tsx
import { Slider } from "@heroui/react";

function SliderExample() {
    return (
        <Slider defaultValue={50}>
            <Slider.Label>Volume</Slider.Label>
            <Slider.Track>
                <Slider.Range />
                <Slider.Thumb />
            </Slider.Track>
            <Slider.ValueLabel />
        </Slider>
    );
}
```

### Controlled Slider

```tsx
import { Slider } from "@heroui/react";
import { useState } from "react";

function ControlledSlider() {
    const [volume, setVolume] = useState(50);

    return (
        <Slider value={volume} onChange={setVolume} min={0} max={100}>
            <Slider.Label>Volume: {volume}%</Slider.Label>
            <Slider.Track>
                <Slider.Range />
                <Slider.Thumb />
            </Slider.Track>
        </Slider>
    );
}
```

### Range Slider (Two Thumbs)

```tsx
import { Slider } from "@heroui/react";
import { useState } from "react";

function RangeSlider() {
    const [range, setRange] = useState([20, 80]);

    return (
        <Slider value={range} onChange={setRange} min={0} max={100}>
            <Slider.Label>
                Price Range: ${range[0]} - ${range[1]}
            </Slider.Label>
            <Slider.Track>
                <Slider.Range />
                <Slider.Thumb index={0} />
                <Slider.Thumb index={1} />
            </Slider.Track>
        </Slider>
    );
}
```

### With Steps

```tsx
<Slider defaultValue={50} min={0} max={100} step={10}>
    <Slider.Label>Brightness</Slider.Label>
    <Slider.Track>
        <Slider.Range />
        <Slider.Thumb />
    </Slider.Track>
    <Slider.ValueLabel />
</Slider>
```

### Vertical Slider

```tsx
<Slider orientation="vertical" defaultValue={50} className="h-64">
    <Slider.Label>Volume</Slider.Label>
    <Slider.Track>
        <Slider.Range />
        <Slider.Thumb />
    </Slider.Track>
    <Slider.ValueLabel />
</Slider>
```

### With Marks

```tsx
<Slider
    defaultValue={50}
    min={0}
    max={100}
    marks={[
        { value: 0, label: "0%" },
        { value: 25, label: "25%" },
        { value: 50, label: "50%" },
        { value: 75, label: "75%" },
        { value: 100, label: "100%" },
    ]}
>
    <Slider.Track>
        <Slider.Range />
        <Slider.Thumb />
    </Slider.Track>
</Slider>
```

---

## Toggle Component

**Docs**: https://v3.heroui.com/docs/components/toggle  
**Import**: `import { Toggle } from '@heroui/react';`

Button that can be pressed (on/off state like Switch).

### Usage

```tsx
import { Toggle } from "@heroui/react";

<Toggle>
    <Toggle.Icon />
    Toggle me
</Toggle>;
```

### Controlled Toggle

```tsx
import { Toggle } from "@heroui/react";
import { useState } from "react";

function ControlledToggle() {
    const [pressed, setPressed] = useState(false);

    return (
        <Toggle isPressed={pressed} onChange={setPressed}>
            {pressed ? "On" : "Off"}
        </Toggle>
    );
}
```

---

## ToggleGroup Component

**Docs**: https://v3.heroui.com/docs/components/toggle-group  
**Import**: `import { ToggleGroup, Toggle } from '@heroui/react';`

Group of toggles with single or multiple selection.

### Usage

```tsx
import { ToggleGroup, Toggle } from "@heroui/react";

function ToggleGroupExample() {
    const [value, setValue] = useState("left");

    return (
        <ToggleGroup value={value} onChange={setValue} type="single">
            <Toggle value="left">Left</Toggle>
            <Toggle value="center">Center</Toggle>
            <Toggle value="right">Right</Toggle>
        </ToggleGroup>
    );
}
```

### Multiple Selection

```tsx
function MultipleToggleGroup() {
    const [values, setValues] = useState(["bold"]);

    return (
        <ToggleGroup value={values} onChange={setValues} type="multiple">
            <Toggle value="bold">Bold</Toggle>
            <Toggle value="italic">Italic</Toggle>
            <Toggle value="underline">Underline</Toggle>
        </ToggleGroup>
    );
}
```

---

## Common Patterns

### Settings Panel

```tsx
function SettingsPanel() {
    const [settings, setSettings] = useState({
        notifications: true,
        autoSave: false,
        darkMode: true,
    });

    return (
        <div className="space-y-4">
            <h2>Settings</h2>

            <SwitchGroup>
                <Switch
                    isSelected={settings.notifications}
                    onChange={(val) => setSettings({ ...settings, notifications: val })}
                >
                    <Switch.Control>
                        <Switch.Thumb />
                    </Switch.Control>
                    <div>
                        <Label>Email Notifications</Label>
                        <Description>Receive email updates</Description>
                    </div>
                </Switch>

                <Switch
                    isSelected={settings.autoSave}
                    onChange={(val) => setSettings({ ...settings, autoSave: val })}
                >
                    <Switch.Control>
                        <Switch.Thumb />
                    </Switch.Control>
                    <div>
                        <Label>Auto-save</Label>
                        <Description>Automatically save your work</Description>
                    </div>
                </Switch>

                <Switch
                    isSelected={settings.darkMode}
                    onChange={(val) => setSettings({ ...settings, darkMode: val })}
                >
                    <Switch.Control>
                        <Switch.Thumb />
                    </Switch.Control>
                    <div>
                        <Label>Dark Mode</Label>
                        <Description>Use dark theme</Description>
                    </div>
                </Switch>
            </SwitchGroup>
        </div>
    );
}
```

### Volume Control

```tsx
function VolumeControl() {
    const [volume, setVolume] = useState(50);
    const [muted, setMuted] = useState(false);

    return (
        <div className="space-y-4">
            <Switch isSelected={!muted} onChange={(val) => setMuted(!val)}>
                <Switch.Control>
                    <Switch.Thumb />
                </Switch.Control>
                <Label>Mute</Label>
            </Switch>

            <Slider value={volume} onChange={setVolume} isDisabled={muted} min={0} max={100}>
                <Slider.Label>Volume: {volume}%</Slider.Label>
                <Slider.Track>
                    <Slider.Range />
                    <Slider.Thumb />
                </Slider.Track>
            </Slider>
        </div>
    );
}
```

### Price Range Filter

```tsx
function PriceFilter() {
    const [priceRange, setPriceRange] = useState([0, 1000]);

    return (
        <Slider value={priceRange} onChange={setPriceRange} min={0} max={1000} step={10}>
            <Slider.Label>
                Price Range: ${priceRange[0]} - ${priceRange[1]}
            </Slider.Label>
            <Slider.Track>
                <Slider.Range />
                <Slider.Thumb index={0} />
                <Slider.Thumb index={1} />
            </Slider.Track>
        </Slider>
    );
}
```

### Text Formatting Toolbar

```tsx
function TextToolbar() {
    const [formats, setFormats] = useState(["bold"]);

    return (
        <ToggleGroup value={formats} onChange={setFormats} type="multiple">
            <Toggle value="bold">
                <BoldIcon />
            </Toggle>
            <Toggle value="italic">
                <ItalicIcon />
            </Toggle>
            <Toggle value="underline">
                <UnderlineIcon />
            </Toggle>
            <Toggle value="strikethrough">
                <StrikethroughIcon />
            </Toggle>
        </ToggleGroup>
    );
}
```

---

## Best Practices

1. **Use Switch for binary settings** (on/off, enable/disable)
2. **Use Slider for continuous values** (volume, brightness, price range)
3. **Use Toggle for toolbar actions** (bold, italic, alignment)
4. **Provide clear labels** for all controls
5. **Show current value** for sliders (numeric display)
6. **Use descriptions** for complex settings
7. **Group related switches** with SwitchGroup
8. **Provide visual feedback** for state changes
9. **Use appropriate step values** for sliders
10. **Test keyboard navigation** (Tab, Arrow keys, Space)

---

## Accessibility Notes

-   **Switch**: Space/Enter to toggle, label association
-   **Slider**: Arrow keys to adjust, Home/End for min/max
-   **Toggle**: Space/Enter to toggle, ARIA pressed state
-   **Labels**: Always provide accessible labels
-   **Focus indicators**: Visible keyboard focus
-   **ARIA roles**: `role="switch"`, `role="slider"`, `role="group"`
-   **Value announcements**: Screen readers announce current value
-   **Disabled state**: Properly communicated to assistive tech

---

## When to Use This Skill

Use this skill when:

-   Building settings/preferences pages
-   Creating filter controls
-   Implementing volume/brightness controls
-   Building toggle buttons for toolbars
-   Creating range selectors (price, date)
-   Implementing on/off switches
-   Building text formatting toolbars
-   Creating audio/video controls
-   Implementing slider-based filters
