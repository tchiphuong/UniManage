# UniManage Automation Test

## Giới thiệu

**UniManage Auto Test** là bộ mã nguồn kiểm thử tự động (Automation Testing) dành cho hệ thống **UniManage**, giúp đảm bảo chất lượng phần mềm (QA/QC), phát hiện lỗi sớm trong quy trình phát triển và kiểm tra tự động trước khi chuyển giao (CI/CD).

Bộ kiểm thử bao gồm:
- **API Testing**: Kiểm thử các dịch vụ backend API (RESTful, Auth/JWT).
- **UI Web Testing**: Kiểm thử giao diện & luồng người dùng trên Web Dashboard (Next.js).
- **Mobile Testing**: Kiểm thử giao diện & chức năng trên ứng dụng di động (iOS/Android).
- **Performance / Load Testing**: Kiểm thử hiệu năng và tải của hệ thống.

---

## 🛠 Công nghệ & Thư viện sử dụng

- **Core Framework**: [Playwright](https://playwright.dev/) / [Cypress](https://www.cypress.io/) (UI Web & API)
- **Mobile Automation**: [Appium](https://appium.io/) (Mobile iOS & Android)
- **Performance Testing**: [k6](https://k6.io/) / [JMeter](https://jmeter.apache.org/)
- **Language**: TypeScript / C#
- **Test Runner**: Playwright Test Runner / Jest / NUnit
- **Reporting**: Allure Report / HTML Reporter

---

## 📁 Cấu trúc thư mục dự kiến

```text
auto_test/
├── api-tests/              # Kiểm thử tự động dành cho Backend API
│   ├── collections/        # Test suites cho từng module API (System, HR, Auth,...)
│   ├── fixtures/           # Dữ liệu giả định (Mock Data, Payload JSON)
│   └── helpers/            # Hàm hỗ trợ gọi API, login, lấy JWT token
├── ui-web-tests/           # Kiểm thử tự động E2E cho Web Dashboard
│   ├── pages/              # Page Object Model (POM) (LoginPage, UserPage,...)
│   ├── specs/              # Scenarios & Test cases (login.spec.ts, users.spec.ts,...)
│   └── utils/              # Base setup, browser contexts, selectors
├── mobile-tests/           # Kiểm thử tự động cho Mobile App
│   ├── page-objects/       # Page Objects cho màn hình di động
│   ├── specs/              # Test cases di động (Android / iOS)
│   └── config/             # Cấu hình Appium capabilities
├── performance-tests/      # Kịch bản kiểm thử hiệu năng với k6 / JMeter
│   └── scenarios/          # Load test scripts (.js / .jmx)
├── test-results/           # Lưu kết quả test, screenshots & videos khi chạy
├── playwright.config.ts    # Cấu hình chính của Playwright
├── package.json            # Thư viện & kịch bản chạy test
└── README.md               # Tài liệu hướng dẫn (File này)
```

---

## 🚀 Hướng dẫn Cài đặt & Chạy Test

### Yêu cầu tiên quyết

- **Node.js**: >= 20.x
- **Browsers**: Chromium, Firefox, WebKit (sẽ được tự động cài đặt bởi Playwright)
- **Backend & Frontend**: Phải được khởi chạy trước khi thực hiện test E2E.

---

### 1. Cài đặt Dependencies

```bash
cd auto_test
npm install
```

Cài đặt trình duyệt cho Playwright (nếu dùng Playwright):
```bash
npx playwright install
```

---

### 2. Cấu hình môi trường

Tạo file `.env` từ mẫu:

```env
BASE_URL=http://localhost:3000
API_URL=http://localhost:5297/api/v1
IDENTITY_URL=http://localhost:5000
TEST_USER_EMAIL=admin@unimanage.local
TEST_USER_PASSWORD=YourPassword123!
```

---

### 3. Thực thi Test (Run Tests)

#### 🧪 Chạy toàn bộ Test Cases UI:
```bash
npm run test
```

#### 🖥 Chạy Test với giao diện trực quan (UI Mode):
```bash
npx playwright test --ui
```

#### 🌐 Chạy Test trên một trình duyệt cụ thể:
```bash
npx playwright test --project=chromium
```

#### 📡 Chạy riêng API Tests:
```bash
npm run test:api
```

#### 📱 Chạy Mobile Automation Tests:
```bash
npm run test:mobile
```

---

## 📊 Báo cáo kết quả (Test Reports)

Sau khi chạy xong test, xem báo cáo chi tiết dạng HTML bằng lệnh:

```bash
npx playwright show-report
```

Báo cáo bao gồm:
- Danh sách Test Cases (Passed/Failed/Skipped).
- Ảnh chụp màn hình (Screenshots) tại thời điểm bị lỗi.
- Video ghi lại quá trình thực thi test case.

---

## 📝 Quy chuẩn viết Test (Best Practices)

1. **Page Object Model (POM)**: Luôn tách riêng logic tìm element/thao tác giao diện ra các file Page Class.
2. **Independent Tests**: Mỗi test case phải độc lập, không phụ thuộc dữ liệu của test case khác.
3. **Clean Data**: Tự tạo và tự dọn dẹp dữ liệu test (nếu cần) trước/sau khi chạy test (Hooks `beforeEach`, `afterEach`).
4. **Descriptive Naming**: Đặt tên test case rõ ràng, diễn giải đúng kịch bản kiểm thử (ví dụ: `should show error message when login with wrong password`).
