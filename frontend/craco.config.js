/* eslint-disable @typescript-eslint/no-var-requires */
const path = require('path');

module.exports = {
    webpack: {
        alias: {
            '@components': path.resolve(__dirname, 'src/components/'),
            '@contexts': path.resolve(__dirname, 'src/contexts/'),
            '@lib': path.resolve(__dirname, 'src/lib/'),
            '@models': path.resolve(__dirname, 'src/models/'),
            '@test': path.resolve(__dirname, 'src/test/'),
        },
    },
};
