import { expect, type Locator, type Page } from '@playwright/test';

export class LoginPage {
  readonly page: Page;
  readonly usernameInput: Locator;
  readonly passwordInput: Locator;
  readonly loginButton: Locator;
  readonly errorMessage: Locator;

  constructor(page: Page) {
    this.page = page;
    this.usernameInput = page.locator('input[name="username"]');
    this.passwordInput = page.locator('input[name="password"]');
    this.loginButton = page.locator('button[type="submit"]');
    this.errorMessage = page.locator('div.bg-danger-50.text-danger-600');
  }

  async goto() {
    await this.page.goto('/auth/login');
  }

  async login(username?: string, password?: string) {
    if (username) await this.usernameInput.fill(username);
    if (password) await this.passwordInput.fill(password);
    await this.loginButton.click();
  }

  async expectErrorMessageVisible() {
    await expect(this.errorMessage).toBeVisible({ timeout: 5000 });
  }

  async expectValidationErrorVisible() {
    const errorTexts = this.page.locator('div.text-danger');
    await expect(errorTexts.first()).toBeVisible();
  }
}
