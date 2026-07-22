---
name: unimanage-qa-playwright
description: Advanced QA & Automation Testing skill for UniManage using Playwright, Page Object Model (POM), REST API testing, and Gherkin test scenarios. Use when writing, debugging, or planning test cases for UniManage.
---

# UniManage QA & Test Automation Skill

## Overview

Skill này hướng dẫn quy trình kiểm thử tự động (Test Automation) và kiểm thử thủ công (Manual QA) chuẩn quốc tế áp dụng cho dự án UniManage.

---

## 1. Nguyên Tắc Kiểm Thử (QA Principles)

1. **Page Object Model (POM)**: Tất cả mã test Playwright E2E **bắt buộc** tách biệt giữa UI Locators/Actions (Page Objects) và Test Specs.
2. **Không Hardcode Credentials / Locators**: Lấy tài khoản đăng nhập từ `process.env`. Ưu tiên locator theo ARIA role (`getByRole`), label (`getByLabel`), test-id (`getByTestId`).
3. **API Validation**: Luôn verify `returnCode === 0` đối với cấu trúc phản hồi `Result<T>` của UniManage API.
4. **Cleanup Data**: Đảm bảo dọn dẹp dữ liệu test (test data cleanup) sau mỗi lượt chạy test để tránh ô nhiễm CSDL.

---

## 2. Cấu Trúc Thư Mục Test (`e2e-tests/` & `auto_test/`)

```
frontend/uni-manage/e2e-tests/
├── pages/                           # Page Object Models
│   ├── base.page.ts                 # Base Page class (navigation, common waits)
│   ├── login.page.ts                # Login Page Object
│   └── users.page.ts                # Users Management Page Object
├── fixtures/                        # Custom Test Fixtures & Auth Setup
├── auth.login.spec.ts               # Test Spec Đăng nhập
└── system.users.spec.ts             # Test Spec Quản lý người dùng
```

---

## 3. Mẫu Khai Báo Page Object Model (POM Example)

```typescript
// e2e-tests/pages/users.page.ts
import { Page, Locator, expect } from '@playwright/test';

export class UsersPage {
  readonly page: Page;
  readonly searchInput: Locator;
  readonly addButton: Locator;
  readonly userTable: Locator;

  constructor(page: Page) {
    this.page = page;
    this.searchInput = page.getByPlaceholder('Tìm kiếm người dùng...');
    this.addButton = page.getByRole('button', { name: 'Thêm người dùng' });
    this.userTable = page.getByRole('table');
  }

  async goto() {
    await this.page.goto('/system/users');
  }

  async searchUser(keyword: string) {
    await this.searchInput.fill(keyword);
    await this.page.waitForTimeout(500); // Debounce wait
  }

  async verifyUserInTable(username: string) {
    await expect(this.userTable).toContainText(username);
  }
}
```

---

## 4. Kịch Bản Test Mẫu Cú Pháp Gherkin (Gherkin Feature Format)

```gherkin
Feature: Quản lý người dùng hệ thống (System Users Management)

  Scenario: Tạo mới người dùng thành công
    Given Người dùng đã đăng nhập hệ thống với vai trò "Administrator"
    And Đang ở màn hình danh sách người dùng "/system/users"
    When Người dùng nhấn nút "+ Thêm người dùng"
    And Nhập tên đăng nhập "testuser_01" và email "testuser01@unimanage.com"
    And Chọn vai trò "Staff" và nhấn nút "Lưu"
    Then Hệ thống hiển thị thông báo "Tạo người dùng thành công"
    And Bảng danh sách xuất hiện bản ghi có tên đăng nhập "testuser_01"
```

---

## 5. Lệnh Chạy Test Playwright

```bash
# Di chuyển vào thư mục frontend
cd frontend/uni-manage

# Chạy tất cả E2E Test (Headless mode)
npx playwright test

# Chạy có giao diện trình duyệt (UI mode)
npx playwright test --ui

# Chạy riêng 1 file test spec
npx playwright test e2e-tests/system.users.spec.ts
```
