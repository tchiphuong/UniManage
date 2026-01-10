"use client";

import { useState } from "react";
import { useTranslations } from "next-intl";
import {
    Button,
    Form,
    Input,
    Label,
    TextField,
    FieldError,
} from "@heroui/react";
import { Tabs, Switch, SettingCard, ColorPicker } from "@/components/common";
import type { SystemSettings } from "@/types";

const SaveIcon = () => (
    <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M13.3333 14H2.66667C2.29848 14 2 13.7015 2 13.3333V2.66667C2 2.29848 2.29848 2 2.66667 2H10.6667L14 5.33333V13.3333C14 13.7015 13.7015 14 13.3333 14Z" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        <path d="M11.3333 14V8.66667H4.66667V14" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        <path d="M4.66667 2V5.33333H10" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
    </svg>
);

export default function SettingsPage() {
    const t = useTranslations("settings");
    const [activeTab, setActiveTab] = useState("general");
    const [isSaving, setIsSaving] = useState(false);

    // Mock initial settings - replace with API call
    const [settings, setSettings] = useState<SystemSettings>({
        general: {
            systemName: "UniManage",
            timezone: "Asia/Ho_Chi_Minh",
            defaultLanguage: "vi",
            dateFormat: "DD/MM/YYYY",
            timeFormat: "24h",
        },
        email: {
            smtpHost: "smtp.gmail.com",
            smtpPort: 587,
            smtpUsername: "",
            smtpPassword: "",
            smtpUseSsl: true,
            fromEmail: "noreply@unimanage.com",
            fromName: "UniManage System",
        },
        security: {
            passwordMinLength: 8,
            passwordRequireUppercase: true,
            passwordRequireLowercase: true,
            passwordRequireNumbers: true,
            passwordRequireSpecialChars: true,
            sessionTimeout: 30,
            maxLoginAttempts: 5,
            lockoutDuration: 15,
            enableTwoFactor: false,
        },
        appearance: {
            theme: "light",
            primaryColor: "#0070f3",
            accentColor: "#7928ca",
            borderRadius: "medium",
        },
    });

    const handleSave = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsSaving(true);

        try {
            // TODO: Call API to save settings
            await new Promise(resolve => setTimeout(resolve, 1000));
            console.log("Settings saved:", settings);
            // Show success message
        } catch (error) {
            console.error("Failed to save settings:", error);
            // Show error message
        } finally {
            setIsSaving(false);
        }
    };

    return (
        <div className="p-6 max-w-6xl mx-auto">
            <div className="mb-6">
                <h1 className="text-3xl font-bold text-foreground">System Settings</h1>
                <p className="text-default-500 mt-2">Manage your system configuration and preferences</p>
            </div>

            <Form onSubmit={handleSave}>
                <Tabs
                    selectedKey={activeTab}
                    onSelectionChange={(key) => setActiveTab(key as string)}
                    className="w-full"
                >
                    <Tabs.List className="flex gap-2 border-b border-default-200 mb-6">
                        <Tabs.Tab
                            id="general"
                            className="px-4 py-2 text-sm font-medium data-[selected=true]:border-b-2 data-[selected=true]:border-primary data-[selected=true]:text-primary"
                        >
                            General
                        </Tabs.Tab>
                        <Tabs.Tab
                            id="email"
                            className="px-4 py-2 text-sm font-medium data-[selected=true]:border-b-2 data-[selected=true]:border-primary data-[selected=true]:text-primary"
                        >
                            Email
                        </Tabs.Tab>
                        <Tabs.Tab
                            id="security"
                            className="px-4 py-2 text-sm font-medium data-[selected=true]:border-b-2 data-[selected=true]:border-primary data-[selected=true]:text-primary"
                        >
                            Security
                        </Tabs.Tab>
                        <Tabs.Tab
                            id="appearance"
                            className="px-4 py-2 text-sm font-medium data-[selected=true]:border-b-2 data-[selected=true]:border-primary data-[selected=true]:text-primary"
                        >
                            Appearance
                        </Tabs.Tab>
                    </Tabs.List>

                    <Tabs.Panel id="general" className="py-4">
                        <div className="bg-white dark:bg-gray-800 rounded-lg p-6 shadow-sm space-y-6">
                            <div>
                                <h2 className="text-xl font-semibold mb-4">General Settings</h2>
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                    <TextField className="flex flex-col gap-1.5" name="systemName" isRequired>
                                        <Label className="text-sm font-medium">System Name</Label>
                                        <Input
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.general.systemName}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                general: { ...settings.general, systemName: e.target.value }
                                            })}
                                        />
                                        <FieldError className="text-tiny text-danger-500" />
                                    </TextField>

                                    <TextField className="flex flex-col gap-1.5" name="timezone">
                                        <Label className="text-sm font-medium">Timezone</Label>
                                        <Input
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.general.timezone}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                general: { ...settings.general, timezone: e.target.value }
                                            })}
                                        />
                                    </TextField>

                                    <TextField className="flex flex-col gap-1.5" name="defaultLanguage">
                                        <Label className="text-sm font-medium">Default Language</Label>
                                        <Input
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.general.defaultLanguage}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                general: { ...settings.general, defaultLanguage: e.target.value }
                                            })}
                                        />
                                    </TextField>

                                    <TextField className="flex flex-col gap-1.5" name="dateFormat">
                                        <Label className="text-sm font-medium">Date Format</Label>
                                        <Input
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.general.dateFormat}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                general: { ...settings.general, dateFormat: e.target.value }
                                            })}
                                        />
                                    </TextField>
                                </div>
                            </div>
                        </div>
                    </Tabs.Panel>

                    <Tabs.Panel id="email" className="py-4">
                        <div className="bg-white dark:bg-gray-800 rounded-lg p-6 shadow-sm space-y-6">
                            <div>
                                <h2 className="text-xl font-semibold mb-4">Email Configuration</h2>
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                    <TextField className="flex flex-col gap-1.5" name="smtpHost" isRequired>
                                        <Label className="text-sm font-medium">SMTP Host</Label>
                                        <Input
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.email.smtpHost}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                email: { ...settings.email, smtpHost: e.target.value }
                                            })}
                                        />
                                        <FieldError className="text-tiny text-danger-500" />
                                    </TextField>

                                    <TextField className="flex flex-col gap-1.5" name="smtpPort" isRequired>
                                        <Label className="text-sm font-medium">SMTP Port</Label>
                                        <Input
                                            type="number"
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.email.smtpPort.toString()}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                email: { ...settings.email, smtpPort: parseInt(e.target.value) || 587 }
                                            })}
                                        />
                                        <FieldError className="text-tiny text-danger-500" />
                                    </TextField>

                                    <TextField className="flex flex-col gap-1.5" name="smtpUsername">
                                        <Label className="text-sm font-medium">SMTP Username</Label>
                                        <Input
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.email.smtpUsername}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                email: { ...settings.email, smtpUsername: e.target.value }
                                            })}
                                        />
                                    </TextField>

                                    <TextField className="flex flex-col gap-1.5" name="smtpPassword">
                                        <Label className="text-sm font-medium">SMTP Password</Label>
                                        <Input
                                            type="password"
                                            className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                            value={settings.email.smtpPassword}
                                            onChange={(e) => setSettings({
                                                ...settings,
                                                email: { ...settings.email, smtpPassword: e.target.value }
                                            })}
                                        />
                                    </TextField>

                                    <div className="flex items-center gap-3 col-span-2">
                                        <Switch
                                            checked={settings.email.smtpUseSsl}
                                            onChange={(checked) => setSettings({
                                                ...settings,
                                                email: { ...settings.email, smtpUseSsl: checked }
                                            })}
                                        />
                                        <Label className="text-sm font-medium">Use SSL/TLS</Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Tabs.Panel>

                    <Tabs.Panel id="security" className="py-4">
                        <div className="bg-white dark:bg-gray-800 rounded-lg p-6 shadow-sm space-y-6">
                            <div>
                                <h2 className="text-xl font-semibold mb-4">Security Settings</h2>
                                <div className="space-y-4">
                                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                        <TextField className="flex flex-col gap-1.5" name="passwordMinLength">
                                            <Label className="text-sm font-medium">Minimum Password Length</Label>
                                            <Input
                                                type="number"
                                                className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                                value={settings.security.passwordMinLength.toString()}
                                                onChange={(e) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, passwordMinLength: parseInt(e.target.value) || 8 }
                                                })}
                                            />
                                        </TextField>

                                        <TextField className="flex flex-col gap-1.5" name="sessionTimeout">
                                            <Label className="text-sm font-medium">Session Timeout (minutes)</Label>
                                            <Input
                                                type="number"
                                                className="w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                                value={settings.security.sessionTimeout.toString()}
                                                onChange={(e) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, sessionTimeout: parseInt(e.target.value) || 30 }
                                                })}
                                            />
                                        </TextField>
                                    </div>

                                    <div className="space-y-3 pt-4 border-t border-default-200">
                                        <h3 className="text-sm font-semibold text-default-700">Password Requirements</h3>
                                        <div className="flex items-center gap-3">
                                            <Switch
                                                checked={settings.security.passwordRequireUppercase}
                                                onChange={(checked) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, passwordRequireUppercase: checked }
                                                })}
                                            />
                                            <Label className="text-sm">Require uppercase letters</Label>
                                        </div>
                                        <div className="flex items-center gap-3">
                                            <Switch
                                                checked={settings.security.passwordRequireLowercase}
                                                onChange={(checked) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, passwordRequireLowercase: checked }
                                                })}
                                            />
                                            <Label className="text-sm">Require lowercase letters</Label>
                                        </div>
                                        <div className="flex items-center gap-3">
                                            <Switch
                                                checked={settings.security.passwordRequireNumbers}
                                                onChange={(checked) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, passwordRequireNumbers: checked }
                                                })}
                                            />
                                            <Label className="text-sm">Require numbers</Label>
                                        </div>
                                        <div className="flex items-center gap-3">
                                            <Switch
                                                checked={settings.security.passwordRequireSpecialChars}
                                                onChange={(checked) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, passwordRequireSpecialChars: checked }
                                                })}
                                            />
                                            <Label className="text-sm">Require special characters</Label>
                                        </div>
                                        <div className="flex items-center gap-3 pt-2">
                                            <Switch
                                                checked={settings.security.enableTwoFactor}
                                                onChange={(checked) => setSettings({
                                                    ...settings,
                                                    security: { ...settings.security, enableTwoFactor: checked }
                                                })}
                                            />
                                            <Label className="text-sm font-medium">Enable Two-Factor Authentication</Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Tabs.Panel>

                    <Tabs.Panel id="appearance" className="py-4">
                        <div className="bg-white dark:bg-gray-800 rounded-lg p-6 shadow-sm space-y-6">
                            <div>
                                <h2 className="text-xl font-semibold mb-4">Appearance Settings</h2>
                                <div className="space-y-6">
                                    <div>
                                        <Label className="text-sm font-medium mb-3 block">Theme</Label>
                                        <div className="flex gap-4">
                                            {(['light', 'dark', 'auto'] as const).map((theme) => (
                                                <button
                                                    key={theme}
                                                    type="button"
                                                    onClick={() => setSettings({
                                                        ...settings,
                                                        appearance: { ...settings.appearance, theme }
                                                    })}
                                                    className={`px-4 py-2 rounded-lg border-2 transition-all capitalize ${settings.appearance.theme === theme
                                                        ? 'border-primary bg-primary/10 text-primary font-medium'
                                                        : 'border-default-200 hover:border-default-300'
                                                        }`}
                                                >
                                                    {theme}
                                                </button>
                                            ))}
                                        </div>
                                    </div>

                                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                        <div>
                                            <Label className="text-sm font-medium mb-2 block">Primary Color</Label>
                                            <div className="flex gap-2 items-center">
                                                <input
                                                    type="color"
                                                    value={settings.appearance.primaryColor}
                                                    onChange={(e) => setSettings({
                                                        ...settings,
                                                        appearance: { ...settings.appearance, primaryColor: e.target.value }
                                                    })}
                                                    className="w-12 h-12 rounded-lg cursor-pointer"
                                                />
                                                <Input
                                                    value={settings.appearance.primaryColor}
                                                    onChange={(e) => setSettings({
                                                        ...settings,
                                                        appearance: { ...settings.appearance, primaryColor: e.target.value }
                                                    })}
                                                    className="flex-1 px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                                />
                                            </div>
                                        </div>

                                        <div>
                                            <Label className="text-sm font-medium mb-2 block">Accent Color</Label>
                                            <div className="flex gap-2 items-center">
                                                <input
                                                    type="color"
                                                    value={settings.appearance.accentColor}
                                                    onChange={(e) => setSettings({
                                                        ...settings,
                                                        appearance: { ...settings.appearance, accentColor: e.target.value }
                                                    })}
                                                    className="w-12 h-12 rounded-lg cursor-pointer"
                                                />
                                                <Input
                                                    value={settings.appearance.accentColor}
                                                    onChange={(e) => setSettings({
                                                        ...settings,
                                                        appearance: { ...settings.appearance, accentColor: e.target.value }
                                                    })}
                                                    className="flex-1 px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                                                />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Tabs.Panel>
                </Tabs>

                <div className="flex justify-end gap-3 mt-6 pt-6 border-t border-default-200">
                    <Button
                        type="button"
                        className="px-6 py-2 bg-default-100 hover:bg-default-200 rounded-lg font-medium transition-colors"
                    >
                        Cancel
                    </Button>
                    <Button
                        type="submit"
                        className="px-6 py-2 bg-primary hover:bg-primary/90 text-white rounded-lg font-medium transition-colors flex items-center gap-2"
                        isDisabled={isSaving}
                    >
                        <SaveIcon />
                        {isSaving ? "Saving..." : "Save Changes"}
                    </Button>
                </div>
            </Form>
        </div>
    );
}
