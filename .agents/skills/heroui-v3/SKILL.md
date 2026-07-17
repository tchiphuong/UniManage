---
name: HeroUI v3 Frontend Developer
description: Hướng dẫn tiêu chuẩn để phát triển giao diện frontend sử dụng HeroUI v3 (React 19 & Tailwind CSS v4)
---

# HeroUI v3 Guidelines

Đây là tài liệu hướng dẫn bắt buộc (Mandatory Rules) dành cho các AI Agent khi viết mã giao diện (UI) cho dự án frontend (Next.js & Vite) sử dụng thư viện **HeroUI v3** (trước đây là NextUI).

## 1. Công nghệ nền tảng
- **UI Library**: HeroUI v3 (`@heroui/react` phiên bản 3.x trở lên).
- **Styling**: Tailwind CSS v4.
- **Base Components**: Được xây dựng trên nền tảng **React Aria Components**, đảm bảo tối ưu Keyboard Navigation, Focus Management và Accessibility.
- **Framework**: Hỗ trợ đồng thời Next.js 15 (App Router, React 19) và Vite (React-Router, React 18+).

## 2. Quy tắc sử dụng Component

### 2.1. Cấu hình mặc định (Wrappers)
Để đảm bảo tính nhất quán (consistency) toàn hệ thống và tránh giao diện bị bo tròn quá mức, **luôn ưu tiên sử dụng các Component Wrapper** đã được tuỳ chỉnh sẵn trong dự án thay vì import trực tiếp từ `@heroui/react` nếu chúng tồn tại:

- **Input / Button / Table / Select**: 
  - Import từ thư mục: `@/components/ui/...`
  - Ví dụ: `import { Input } from "@/components/ui/input";`
  - Các Wrapper này đã được tự động gán cấu hình: `radius="sm"`, `variant="bordered"` (với Input) để tạo cảm giác chuyên nghiệp, góc cạnh.

### 2.2. Import trực tiếp
Đối với các component chưa có Wrapper (như Dropdown, Avatar, Badge, ScrollShadow, Tooltip, Navbar...), import trực tiếp từ HeroUI:
```tsx
// ✅ ĐÚNG: Import từ thư viện gộp (ưu tiên)
import { Dropdown, Avatar, Badge, Tooltip, ScrollShadow } from "@heroui/react";

// ❌ SAI: KHÔNG sử dụng nextui-org (phiên bản cũ)
import { Dropdown } from "@nextui-org/react"; 
```

## 3. Quy tắc Thiết kế (Design Principles)

1. **Radius & Sharpness**: 
   - CSS Core của HeroUI trong `hero.js` đã được tinh chỉnh (`radius.medium = "0.375rem"`).
   - Hạn chế ghi đè (override) radius bằng các class tailwind (như `rounded-2xl` hay `rounded-full`) ngoại trừ Avatar hoặc các nhãn dán (Badge) đặc biệt.

2. **Scrollbar & Overflow**:
   - Thay vì dùng `overflow-y-auto` thô kệch, **luôn ưu tiên sử dụng `<ScrollShadow>`** cho các vùng nội dung dài (như danh sách Menu, danh sách thông báo) để tạo hiệu ứng fade mượt mà ở hai đầu vùng cuộn.

3. **Icons**:
   - **Header / Các khu vực tĩnh giản đơn**: Sử dụng thư viện `@heroicons/react` (Outline cho trạng thái bình thường, Solid cho `Active`).
   - **Menu, Dashboard / Các khu vực nổi bật**: Sử dụng bộ icon hình ảnh **Icons8 Liquid Glass Color** (icon 3D có màu sắc).
     - Các file PNG này được lưu trữ cục bộ tại `public/icons/`.
     - Sử dụng thẻ `<img src="/icons/[tên-icon].png" />`.
     - Tuyệt đối không load icon qua URL mạng để đảm bảo hiệu suất.

4. **Biến số và Cấu hình (App Config)**:
   - Các con số mặc định (Page size, Date format, Base URL) **TUYỆT ĐỐI KHÔNG hard-code** vào component.
   - Luôn gọi từ file cấu hình: `import { appConfig } from "@/config/app.config";` hoặc `lib/config/app.config.ts`.

## 4. Đặc thù Next.js vs Vite

Khi viết code sử dụng HeroUI Link hoặc Provider, lưu ý Router khác nhau:
- **Next.js (uni-manage)**: Dùng `useRouter` từ `next/navigation` hoặc `@/i18n/navigation`. Thường kết hợp thẻ `<Link>` của Next.
- **Vite (dashboard_tailwind_repo)**: Dùng `useNavigate` từ `react-router-dom` và thẻ `<Link>` của React-Router.
## 5. HeroUI v3 & Pro Critical Rules (Agent Skills)

Dựa trên bộ quy chuẩn chính thức từ HeroUI Pro (`heroui-react-pro` và `heroui-pro-design-taste`), hãy tuân thủ nghiêm ngặt các nguyên tắc sau:

1. **Event Handlers (React Aria)**:
   - Sử dụng **`onPress`** thay vì `onClick` cho các tương tác nhấp chuột (click/touch). Đây là tiêu chuẩn bắt buộc của React Aria để hỗ trợ đa thiết bị (chuột, cảm ứng, bàn phím) một cách hoàn hảo.

2. **Compound Components**:
   - HeroUI v3 sử dụng mạnh mẽ mô hình Compound Components (ví dụ: `<Dropdown><DropdownTrigger>...</DropdownTrigger><DropdownMenu>...</DropdownMenu></Dropdown>`). Luôn khai báo đầy đủ các thành phần con theo tài liệu.

3. **Styling & CSS System**:
   - Tận dụng hệ thống BEM classes (`base`, `wrapper`, `content`, `label`...) thông qua prop `classNames={{ ... }}` để tuỳ chỉnh sâu vào component thay vì bọc thẻ `div` lồng nhau.
   - Hạn chế sử dụng custom CSS thuần. Tuân thủ thiết kế semantic (ngữ nghĩa) hơn là visual (thị giác).

4. **Design Taste (Triết lý thiết kế)**:
   - **Khoảng trắng (Generous whitespace)**: Luôn để không gian thở (padding/margin) rộng rãi.
   - **Độ sâu (Subtle depth)**: Sử dụng shadow nhẹ nhàng, không dùng shadow quá gắt.
   - **Tối giản (Minimalism)**: Tránh lạm dụng màu sắc nổi bật. Giữ giao diện gọn gàng, sử dụng màu nhấn (Primary/Success/Danger) đúng lúc.

> Tóm tắt: Luôn dùng **HeroUI v3**, ưu tiên dùng **Wrapper trong `@/components/ui`**, dùng `onPress` thay vì `onClick`, và tuân thủ phong cách thiết kế tối giản, nhiều khoảng trắng.

## 6. Production-Grade Automation & E2E Testing

Khi xây dựng mã kiểm thử tự động (Auto Test) cho dự án product, tuyệt đối tuân thủ chuẩn Enterprise:
1. **Công cụ**: Sử dụng **Playwright** cho toàn bộ quá trình E2E Testing.
2. **Page Object Model (POM)**: Tuyệt đối không viết test script lộn xộn (spaghetti code). Phải tách riêng logic tương tác UI ra các class POM đặt trong thư mục `e2e-tests/pages/` để dễ tái sử dụng và bảo trì.
3. **Môi trường & Biến số**: Không hardcode thông tin nhạy cảm (username, password) vào mã nguồn test. Luôn đọc từ biến môi trường (`process.env`) hoặc dùng Fixtures/Mock data.
4. **Bối cảnh (Isolation)**: Tận dụng cơ chế tự động cô lập (isolated context) của Playwright. Mỗi test case (`test()`) tự động có một session/trình duyệt độc lập. Không lạm dụng `page.context().clearCookies()` một cách dư thừa trong `beforeEach`.
5. **CI/CD Ready**: Cấu hình test chạy song song (`fullyParallel: true`) và cơ chế chụp ảnh/quay video khi lỗi (`screenshot: 'only-on-failure'`) để sẵn sàng tích hợp vào Github Actions/Gitlab CI.
