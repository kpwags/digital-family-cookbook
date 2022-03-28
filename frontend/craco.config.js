/* eslint-disable import/no-extraneous-dependencies */
/* eslint-disable @typescript-eslint/no-var-requires */
const path = require('path');
const CracoAntDesignPlugin = require('craco-antd');

module.exports = {
    webpack: {
        alias: {
            '@components': path.resolve(__dirname, 'src/components/'),
            '@contexts': path.resolve(__dirname, 'src/contexts/'),
            '@hooks': path.resolve(__dirname, 'src/hooks/'),
            '@models': path.resolve(__dirname, 'src/models/'),
            '@test': path.resolve(__dirname, 'src/test/'),
            '@utils': path.resolve(__dirname, 'src/utils/'),
        },
    },
    plugins: [
        {
            plugin: CracoAntDesignPlugin,
            options: {
                lessLoaderOptions: {
                    lessOptions: {
                        javascriptEnabled: true,
                        useFileCache: true,
                    },
                },
            },
        },
    ],
};
