import { expect, test } from "@playwright/test";

import { LoginPage } from "./pages/login-page";

test.describe("Authentication - Login Flow", () => {
    let loginPage: LoginPage;

    test.beforeEach(async ({ page }) => {
        // Playwright automatically provides a completely new 'page' (isolated context) for each test
        loginPage = new LoginPage(page);
        await loginPage.goto();
    });

    test("✅ Đăng nhập thành công với tài khoản đúng", async ({ page }) => {
        // Use environment variables if available, fallback to test account
        const testUser = process.env.TEST_USERNAME || "admin";
        const testPass = process.env.TEST_PASSWORD || "admin123";

        await loginPage.login(testUser, testPass);

        // Verify redirection to dashboard
        await expect(page).toHaveURL(/.*\/dashboard/, { timeout: 10000 });
    });

    test("❌ Đăng nhập thất bại - Báo lỗi khi sai mật khẩu", async () => {
        await loginPage.login("admin", "wrongpassword");
        await loginPage.expectErrorMessageVisible();
    });

    test("❌ Validation - Báo lỗi khi để trống thông tin", async () => {
        await loginPage.login("", ""); // Submit immediately
        await loginPage.expectValidationErrorVisible();
    });
});
