import { test, expect } from '@playwright/test';
import { LoginPage } from './pages/LoginPage';

test.describe('Authentication - Login Flow', () => {
  let loginPage: LoginPage;

  test.beforeEach(async ({ page }) => {
    // Playwright tự động cung cấp 'page' mới hoàn toàn (isolated context) cho mỗi test
    loginPage = new LoginPage(page);
    await loginPage.goto();
  });

  test('✅ Đăng nhập thành công với tài khoản đúng', async ({ page }) => {
    // Sử dụng thông tin từ biến môi trường nếu có, fallback về account test
    const testUser = process.env.TEST_USERNAME || 'admin';
    const testPass = process.env.TEST_PASSWORD || 'admin123';

    await loginPage.login(testUser, testPass);

    // Xác nhận đã chuyển hướng vào dashboard
    await expect(page).toHaveURL(/.*\/dashboard/, { timeout: 10000 });
  });

  test('❌ Đăng nhập thất bại - Báo lỗi khi sai mật khẩu', async () => {
    await loginPage.login('admin', 'wrongpassword');
    await loginPage.expectErrorMessageVisible();
  });

  test('❌ Validation - Báo lỗi khi để trống thông tin', async () => {
    await loginPage.login('', ''); // Bấm submit ngay lập tức
    await loginPage.expectValidationErrorVisible();
  });
});
