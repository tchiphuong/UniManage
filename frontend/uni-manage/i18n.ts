import { getRequestConfig } from "next-intl/server";

export default getRequestConfig(async ({ requestLocale }) => {
    let locale = await requestLocale;

    // Provide a static locale, fetch a user setting,
    // read from `cookies()`, `headers()`, etc.
    if (!locale) {
        locale = "vi";
    }

    return {
        locale,
        messages: (await import(`./messages/${locale}.json`)).default,
        timeZone: "Asia/Ho_Chi_Minh",
        now: new Date(),
    };
});
