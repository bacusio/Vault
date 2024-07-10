import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
    "./lib/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        "nyt-yellow": "#F5E07E",
        "nyt-green": "#A7C268",
        "nyt-blue": "#B4C3EB",
        "nyt-purple": "#B283C1",
        "nyt-lilac": "#B1A7F8",
        "nyt-light": "#EFEFE6",
        "nyt-dark": "#5A594E",
      },
    },
  },
  plugins: [require("tailwindcss-animate")],
};
export default config;
