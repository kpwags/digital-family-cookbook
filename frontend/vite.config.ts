/* eslint-disable import/no-extraneous-dependencies */
import { defineConfig } from 'vite';
import tsconfigPaths from 'vite-tsconfig-paths';
import react from '@vitejs/plugin-react';
import svgrPlugin from 'vite-plugin-svgr';
import EnvironmentPlugin from 'vite-plugin-environment';

// eslint-disable-next-line @typescript-eslint/no-var-requires
const path = require('path');

export default defineConfig({
    build: {
        outDir: 'build',
    },
    resolve: {
        alias: {
            '@components': path.resolve(__dirname, './src/components/'),
            '@contexts': path.resolve(__dirname, './src/contexts/'),
            '@hooks': path.resolve(__dirname, './src/hooks/'),
            '@models': path.resolve(__dirname, './src/models/'),
            '@test': path.resolve(__dirname, './src/test/'),
            '@utils': path.resolve(__dirname, './src/utils/'),
        },
    },
    plugins: [
        tsconfigPaths,
        react(),
        svgrPlugin({
            svgrOptions: {
                icon: true,
            },
        }),
        EnvironmentPlugin('all'),
    ],
    server: {
        port: 3000,
    },
    css: {
        preprocessorOptions: {
            less: {
                javascriptEnabled: true,
                additionalData: '@root-entry-name: default;',
            },
        },
    },
});
