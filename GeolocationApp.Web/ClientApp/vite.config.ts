import {defineConfig} from 'vite'
import dotenv from 'dotenv';
import react from '@vitejs/plugin-react'
import * as path from "path";

// https://vite.dev/config/
dotenv.config();
export default defineConfig({
    plugins: [react()],
    define: {
        'process.env': process.env
    },
    server: {
        port: 3000,
    },
    preview: {
        port: 3000,
    },
    resolve: {
        alias: {
            '@api': path.resolve(__dirname, 'src/api'),
            '@models': path.resolve(__dirname, 'src/models'),
            '@services': path.resolve(__dirname, 'src/services'),
            '@hooks': path.resolve(__dirname, 'src/hooks'),
            '@components': path.resolve(__dirname, 'src/components'),
            '@utils': path.resolve(__dirname, 'src/utils'),
            '@routes': path.resolve(__dirname, 'src/router'),
            '@pages': path.resolve(__dirname, 'src/page'),
            '@layouts': path.resolve(__dirname, 'src/layouts'),
        },
    }
})
