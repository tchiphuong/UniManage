# UniManage Mobile App (Mobile Web App / PWA & Native)

## Giới thiệu

**UniManage Mobile** là ứng dụng di động thuộc hệ thống quản lý doanh nghiệp **UniManage**, giúp người dùng (nhân viên, quản lý) truy cập và thao tác công việc nhanh chóng trên các thiết bị di động (iOS & Android).

---

## 📱 Định hướng Kiến trúc Multi-Platform

Hệ thống hỗ trợ 2 hình thức triển khai linh hoạt:

1. **iOS (Mobile Web App / PWA)**:
   - Sử dụng mô hình **Progressive Web App (PWA)** hoặc Web App dành riêng cho iOS Safari.
   - Giao diện responsive tối ưu chạm/lướt (Mobile UX/UI).
   - Hỗ trợ tính năng **"Add to Home Screen" (Thêm vào Màn hình chính)** trên iOS Safari để chạy ở chế độ **Standalone (Toàn màn hình)** giống hệt ứng dụng native mà không cần thông qua Apple App Store.
   - Hỗ trợ Offline Caching qua Service Worker, Web Push Notifications (iOS 16.4+).

2. **Android & Cross-Platform (Native / Hybrid App)**:
   - Phát triển ứng dụng bằng **React Native (Expo)** / **Capacitor** nếu cần đóng gói file cài đặt (APK/AAB) hoặc khai thác sâu phần cứng thiết bị.

---

## 🛠 Công nghệ sử dụng

### 🌐 Mobile Web App / PWA (Ưu tiên iOS & Mobile Web)
- **Framework**: Next.js (App Router) / React (Vite)
- **UI Library**: HeroUI v3 (Tailwind CSS v4) tối ưu giao diện Touch & Mobile
- **PWA Features**: Service Worker, Web App Manifest (`manifest.json`), Apple Touch Icons, Push API
- **State & Data**: Zustand / TanStack Query (React Query) v5
- **HTTP Client**: Axios (Kết nối IdentityServer & WebApi)

### 📲 Hybrid / Native App (Option dành cho Android/Native)
- **Framework**: React Native (Expo) / Capacitor Web View Wrapper
- **Language**: TypeScript
- **Styling**: NativeWind / HeroUI Native

---

## 📁 Cấu trúc thư mục

```text
mobile_app/
├── public/                 # Static assets, manifest.json, service-worker.js, apple-touch-icons
│   ├── icons/              # App icons (192x192, 512x512, apple-touch-icon.png)
│   └── manifest.json       # PWA Web App Manifest (display: standalone, theme_color,...)
├── src/
│   ├── api/                # Axios client & API endpoints
│   ├── components/         # Mobile UI Components (HeaderBar, BottomNavigation, Drawer,...)
│   ├── constants/          # Colors, Configs, API URLs
│   ├── hooks/              # Custom React hooks (usePWA, useNetworkStatus,...)
│   ├── i18n/               # Đa ngôn ngữ (VI/EN)
│   ├── pages/ (hoặc app/)  # Các trang di động (Dashboard, Profile, Tasks, Approvals,...)
│   ├── services/           # Authentication & Data Services
│   ├── store/              # Global state management
│   ├── styles/             # Global CSS / Touch styles
│   └── utils/              # Helper utilities
├── .env.example            # Biến môi trường mẫu
├── package.json            # Danh sách dependencies
└── README.md               # Tài liệu hướng dẫn (File này)
```

---

## 📲 Hướng dẫn Cài đặt & Trải nghiệm trên iOS (Mobile Web App / PWA)

### 1. Khởi chạy Server

```bash
cd mobile_app
npm install
npm run dev
```

Ứng dụng sẽ chạy tại địa chỉ local (Ví dụ: `http://192.168.1.x:3000`).

---

### 2. Trải nghiệm trên thiết bị iOS (iPhone / iPad)

1. **Mở Safari** trên thiết bị iOS và truy cập URL của hệ thống (Ví dụ: `https://mobile.unimanage.local` hoặc địa chỉ IP LAN).
2. **Thêm vào Màn hình chính (Add to Home Screen)**:
   - Nhấn vào biểu tượng **Chia sẻ (Share)** 📤 ở thanh điều hướng Safari.
   - Cuộn xuống và chọn **"Thêm vào Màn hình chính" (Add to Home Screen)** ➕.
   - Nhấn **"Thêm" (Add)** ở góc trên bên phải.
3. **Mở ứng dụng**:
   - Biểu tượng **UniManage** sẽ xuất hiện trên màn hình ứng dụng của iOS.
   - Khi mở lên, ứng dụng sẽ chạy ở chế độ **Standalone Mode (Toàn màn hình, không có thanh địa chỉ Safari)** mang lại trải nghiệm mượt mà như app cài từ App Store.

---

## 🤖 Hướng dẫn cho Android / Hybrid App (Nếu dùng Capacitor / Expo)

Nếu đóng gói ứng dụng cho Android qua Capacitor Web Wrapper:

```bash
# Thêm Android platform
npx cap add android

# Build web assets & sync sang Android
npm run build
npx cap sync

# Mở Android Studio để build APK
npx cap open android
```

---

## 🔐 Xác thực & Bảo mật (Authentication)

1. **OAuth2 / OIDC**: Đăng nhập bằng IdentityServer via PKCE Flow hoặc Resource Owner Password Credentials.
2. **Lưu trữ Session**:
   - Web App (iOS PWA): Sử dụng `IndexedDB` / `localStorage` kết hợp `HttpOnly Cookie` hoặc Token Caching.
   - Native App: Sử dụng `SecureStore` / `EncryptedStorage`.
3. **API Requests**: Tự động đính kèm `Authorization: Bearer <token>` vào mọi request qua Axios Interceptors.

---

## 📝 Quy chuẩn phát triển (Guidelines cho iOS Mobile Web)

- **Touch UX**: Thiết kế nút bấm và khoảng cách cảm ứng tối thiểu `44x44px` theo tiêu chuẩn iOS Human Interface Guidelines.
- **Safe Area Insets**: Hỗ trợ Notch / Dynamic Island của iPhone bằng CSS:
  ```css
  padding-top: env(safe-area-inset-top);
  padding-bottom: env(safe-area-inset-bottom);
  ```
- **Disable Zoom**: Cấu hình Viewport để tránh phóng to ngoài ý muốn khi nhấn vào Input:
  ```html
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, viewport-fit=cover">
  ```
