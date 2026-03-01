---
name: heroui-forms
description: Hero UI Form components (Input, TextField, Checkbox, RadioGroup, Select, TextArea, etc). Use when building forms, handling user input, validation in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Form Components

Complete guide for Hero UI Form components. For detailed examples, visit the official documentation links below.

## Input Component

**Docs**: https://v3.heroui.com/docs/components/input  
**Import**: `import { Input } from '@heroui/react';`

Primitive single-line text input accepting standard HTML attributes.

### Key Props

-   `type`: `'text' | 'email' | 'password' | 'number' | 'tel' | 'url'` (default: `'text'`)
-   `value` / `defaultValue`: Controlled/uncontrolled value
-   `onChange`: `(e: ChangeEvent<HTMLInputElement>) => void`
-   `placeholder`, `disabled`, `readOnly`, `required`
-   `maxLength`, `minLength`, `pattern`, `min`, `max`, `step`
-   `fullWidth`: `boolean` - Take full width
-   `isOnSurface`: `boolean` - Surface styling

### Usage

```tsx
import { Input } from '@heroui/react';

<Input placeholder="Enter your name" />
<Input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
<Input type="password" required />
```

**Note**: For validation props (`isInvalid`, `isRequired`, error messages), use **TextField** instead.

---

## TextField Component

**Docs**: https://v3.heroui.com/docs/components/text-field  
**Import**: `import { TextField } from '@heroui/react';`

Full-featured input with label, validation, and error handling.

### Key Props

-   All Input props plus:
-   `label`: `string` - Field label
-   `description`: `string` - Helper text
-   `errorMessage`: `string` - Error message to display
-   `isInvalid`: `boolean` - Validation state
-   `isRequired`: `boolean` - Required indicator

### Usage

```tsx
import { TextField } from '@heroui/react';

<TextField
  label="Email"
  type="email"
  required
  errorMessage={errors.email}
/>

<TextField
  label="Username"
  description="Choose a unique username"
  isInvalid={!!errors.username}
/>
```

---

## Checkbox Component

**Docs**: https://v3.heroui.com/docs/components/checkbox  
**Import**: `import { Checkbox } from '@heroui/react';`

### Usage

```tsx
import { Checkbox, Label } from "@heroui/react";

<Checkbox>
    <Checkbox.Control>
        <Checkbox.Indicator />
    </Checkbox.Control>
    <Label>Accept terms</Label>
</Checkbox>;

// With state
const [checked, setChecked] = useState(false);
<Checkbox checked={checked} onChange={setChecked}>
    <Checkbox.Control>
        <Checkbox.Indicator />
    </Checkbox.Control>
    <Label>Subscribe to newsletter</Label>
</Checkbox>;
```

---

## CheckboxGroup Component

**Docs**: https://v3.heroui.com/docs/components/checkbox-group  
**Import**: `import { CheckboxGroup } from '@heroui/react';`

Group multiple checkboxes with shared state.

### Usage

```tsx
<CheckboxGroup value={selected} onChange={setSelected}>
    <Checkbox value="option1">
        <Label>Option 1</Label>
    </Checkbox>
    <Checkbox value="option2">
        <Label>Option 2</Label>
    </Checkbox>
    <Checkbox value="option3">
        <Label>Option 3</Label>
    </Checkbox>
</CheckboxGroup>
```

---

## RadioGroup Component

**Docs**: https://v3.heroui.com/docs/components/radio-group  
**Import**: `import { RadioGroup, Radio } from '@heroui/react';`

Single selection from multiple options.

### Usage

```tsx
import { RadioGroup, Radio, Label } from "@heroui/react";

<RadioGroup value={selected} onChange={setSelected}>
    <Radio value="option1">
        <Label>Option 1</Label>
    </Radio>
    <Radio value="option2">
        <Label>Option 2</Label>
    </Radio>
    <Radio value="option3">
        <Label>Option 3</Label>
    </Radio>
</RadioGroup>;
```

---

## Select Component

**Docs**: https://v3.heroui.com/docs/components/select  
**Import**: `import { Select } from '@heroui/react';`

Dropdown selection with search and filtering.

### Usage

```tsx
import { Select } from '@heroui/react';

<Select
  label="Country"
  placeholder="Select a country"
>
  <Select.Item key="us">United States</Select.Item>
  <Select.Item key="uk">United Kingdom</Select.Item>
  <Select.Item key="ca">Canada</Select.Item>
</Select>

// With state
<Select
  selectedKey={country}
  onSelectionChange={setCountry}
>
  {/* items */}
</Select>
```

---

## ComboBox Component

**Docs**: https://v3.heroui.com/docs/components/combobox  
**Import**: `import { ComboBox } from '@heroui/react';`

Searchable dropdown with autocomplete.

### Usage

```tsx
<ComboBox label="Search users" placeholder="Type to search...">
    <ComboBox.Item key="1">John Doe</ComboBox.Item>
    <ComboBox.Item key="2">Jane Smith</ComboBox.Item>
</ComboBox>
```

---

## TextArea Component

**Docs**: https://v3.heroui.com/docs/components/textarea  
**Import**: `import { TextArea } from '@heroui/react';`

Multi-line text input.

### Usage

```tsx
<TextArea label="Description" placeholder="Enter description..." rows={4} maxLength={500} />
```

---

## SearchField Component

**Docs**: https://v3.heroui.com/docs/components/search-field  
**Import**: `import { SearchField } from '@heroui/react';`

Specialized input for search functionality.

### Usage

```tsx
<SearchField placeholder="Search..." value={query} onChange={setQuery} />
```

---

## NumberField Component

**Docs**: https://v3.heroui.com/docs/components/number-field  
**Import**: `import { NumberField } from '@heroui/react';`

Number input with increment/decrement buttons.

### Usage

```tsx
<NumberField label="Quantity" min={1} max={100} step={1} value={quantity} onChange={setQuantity} />
```

---

## DateField Component

**Docs**: https://v3.heroui.com/docs/components/date-field  
**Import**: `import { DateField } from '@heroui/react';`

Date input with calendar picker.

### Usage

```tsx
<DateField label="Birth Date" value={date} onChange={setDate} />
```

---

## InputOTP Component

**Docs**: https://v3.heroui.com/docs/components/input-otp  
**Import**: `import { InputOTP } from '@heroui/react';`

One-time password input with multiple boxes.

### Usage

```tsx
<InputOTP length={6} value={otp} onChange={setOtp} />
```

---

## Form Component

**Docs**: https://v3.heroui.com/docs/components/form  
**Import**: `import { Form } from '@heroui/react';`

Form wrapper with validation and submission handling.

### Usage

```tsx
<Form onSubmit={handleSubmit}>
    <TextField label="Email" name="email" required />
    <TextField label="Password" name="password" type="password" required />
    <Button type="submit">Submit</Button>
</Form>
```

---

## Supporting Components

### Label

**Docs**: https://v3.heroui.com/docs/components/label

```tsx
<Label htmlFor="email">Email Address</Label>
```

### Description

**Docs**: https://v3.heroui.com/docs/components/description

```tsx
<Description>Helper text for the field</Description>
```

### ErrorMessage

**Docs**: https://v3.heroui.com/docs/components/error-message

```tsx
<ErrorMessage>{errors.field}</ErrorMessage>
```

### FieldError

**Docs**: https://v3.heroui.com/docs/components/field-error

```tsx
<FieldError name="email">{(error) => error}</FieldError>
```

### Fieldset

**Docs**: https://v3.heroui.com/docs/components/fieldset

```tsx
<Fieldset>
    <legend>Personal Information</legend>
    {/* form fields */}
</Fieldset>
```

---

## Common Form Patterns

### Login Form

```tsx
import { Form, TextField, Button } from "@heroui/react";

function LoginForm() {
    const [formData, setFormData] = useState({ email: "", password: "" });

    return (
        <Form onSubmit={handleSubmit} className="space-y-4">
            <TextField
                label="Email"
                type="email"
                name="email"
                required
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
            />
            <TextField
                label="Password"
                type="password"
                name="password"
                required
                value={formData.password}
                onChange={(e) => setFormData({ ...formData, password: e.target.value })}
            />
            <Button type="submit" fullWidth>
                Login
            </Button>
        </Form>
    );
}
```

### Form with Validation

```tsx
import { TextField, Button } from "@heroui/react";

function SignupForm() {
    const [errors, setErrors] = useState({});

    const validate = (data) => {
        const errors = {};
        if (!data.email.includes("@")) errors.email = "Invalid email";
        if (data.password.length < 8) errors.password = "Password too short";
        return errors;
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <TextField
                label="Email"
                type="email"
                isInvalid={!!errors.email}
                errorMessage={errors.email}
            />
            <TextField
                label="Password"
                type="password"
                isInvalid={!!errors.password}
                errorMessage={errors.password}
                description="At least 8 characters"
            />
            <Button type="submit">Sign Up</Button>
        </form>
    );
}
```

---

## Best Practices

1. **Use TextField over Input** for forms with validation
2. **Always provide labels** for accessibility
3. **Show helpful descriptions** for complex fields
4. **Display clear error messages** with `errorMessage` prop
5. **Use appropriate input types** (email, tel, url, etc.)
6. **Mark required fields** with `required` prop
7. **Group related fields** with Fieldset
8. **Test keyboard navigation** (Tab, Enter, Escape)
9. **Validate on blur** for better UX
10. **Disable submit button** during submission

---

## Accessibility Notes

-   Always provide `label` or `aria-label`
-   Use `description` for helper text (announced by screen readers)
-   Show `errorMessage` for validation errors
-   Ensure proper focus management
-   Support keyboard navigation
-   Provide autocomplete hints where appropriate
-   Use semantic HTML (`<form>`, `<fieldset>`, `<legend>`)

---

## When to Use This Skill

Use this skill when:

-   Building login/signup forms
-   Creating user input interfaces
-   Implementing search functionality
-   Handling form validation
-   Working with checkboxes/radio buttons
-   Building dropdown selections
-   Creating multi-step forms
-   Implementing date/number inputs
