/** @type {import('tailwindcss').Config} */
export default {
    content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
    darkMode: "class",
    theme: {
        extend: {
            animation: {
                "fade-in": "fadeIn 0.6s ease-out forwards",
                "slide-up": "slideUp 0.8s ease-out forwards",
                "slide-down": "slideDown 0.8s ease-out forwards",
                "slide-right": "slideRight 0.8s ease-out forwards",
                "scale-in": "scaleIn 0.5s ease-out forwards",
                "glow-pulse": "glowPulse 2s ease-in-out infinite",
                "float": "float 6s ease-in-out infinite",
                "blob": "blob 7s infinite",
                "gradient-shift": "gradientShift 8s ease infinite",
                "shimmer": "shimmer 2s linear infinite",
                "bounce-slow": "bounce 3s infinite",
                "spin-slow": "spin 20s linear infinite",
            },
            keyframes: {
                fadeIn: {
                    "0%": { opacity: "0" },
                    "100%": { opacity: "1" },
                },
                slideUp: {
                    "0%": { transform: "translateY(30px)", opacity: "0" },
                    "100%": { transform: "translateY(0)", opacity: "1" },
                },
                slideDown: {
                    "0%": { transform: "translateY(-30px)", opacity: "0" },
                    "100%": { transform: "translateY(0)", opacity: "1" },
                },
                slideRight: {
                    "0%": { transform: "translateX(-30px)", opacity: "0" },
                    "100%": { transform: "translateX(0)", opacity: "1" },
                },
                scaleIn: {
                    "0%": { transform: "scale(0.9)", opacity: "0" },
                    "100%": { transform: "scale(1)", opacity: "1" },
                },
                glowPulse: {
                    "0%, 100%": { 
                        boxShadow: "0 0 20px rgba(59, 130, 246, 0.5)",
                    },
                    "50%": { 
                        boxShadow: "0 0 40px rgba(59, 130, 246, 0.8), 0 0 60px rgba(99, 102, 241, 0.6)",
                    },
                },
                float: {
                    "0%, 100%": { transform: "translateY(0px)" },
                    "50%": { transform: "translateY(-20px)" },
                },
                blob: {
                    "0%": { transform: "translate(0px, 0px) scale(1)" },
                    "33%": { transform: "translate(30px, -50px) scale(1.1)" },
                    "66%": { transform: "translate(-20px, 20px) scale(0.9)" },
                    "100%": { transform: "translate(0px, 0px) scale(1)" },
                },
                gradientShift: {
                    "0%, 100%": { backgroundPosition: "0% 50%" },
                    "50%": { backgroundPosition: "100% 50%" },
                },
                shimmer: {
                    "0%": { backgroundPosition: "-1000px 0" },
                    "100%": { backgroundPosition: "1000px 0" },
                },
            },
            backgroundImage: {
                "gradient-radial": "radial-gradient(var(--tw-gradient-stops))",
                "gradient-conic": "conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))",
            },
            backdropBlur: {
                xs: "2px",
            },
        },
    },
    plugins: [],
};
