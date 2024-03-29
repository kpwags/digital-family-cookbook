process.env.BABEL_ENV = 'test';
process.env.NODE_ENV = 'test';

module.exports = {
    preset: 'ts-jest',

    // The root of your source code, typically /src
    // `<rootDir>` is a token Jest substitutes
    roots: ['<rootDir>/src'],

    // Jest transformations -- this adds support for TypeScript
    // using ts-jest
    transform: {
        '^.+\\.tsx?$': 'ts-jest',
        '.*': 'babel-jest',
    },

    setupFiles: [require.resolve('whatwg-fetch')],

    // Runs special logic, such as cleaning up components
    // when using React Testing Library and adds special
    // extended assertions to Jest
    setupFilesAfterEnv: [
        '@testing-library/jest-dom/extend-expect',
        '<rootDir>/src/setupTests.ts',
    ],

    // Test spec file resolution pattern
    // Matches parent folder `__tests__` and filename
    // should contain `test` or `spec`.
    testRegex: '(/__tests__/.*|(\\.|/)(test|spec))\\.tsx?$',

    // Module file extensions for importing
    moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx', 'json', 'node'],

    moduleNameMapper: {
        '\\.(css|less|scss)$': 'identity-obj-proxy',
        '@components/(.*)$': '<rootDir>/src/components/$1',
        '@contexts/(.*)$': '<rootDir>/src/contexts/$1',
        '@hooks/(.*)$': '<rootDir>/src/hooks/$1',
        '@models/(.*)$': '<rootDir>/src/models/$1',
        '@test/(.*)$': '<rootDir>/src/test/$1',
        '@utils/(.*)$': '<rootDir>/src/utils/$1',
    },

    transformIgnorePatterns: [
        '<rootDir>/node_modules/',
    ],

    testPathIgnorePatterns: [
        '<rootDir>/node_modules/',
    ],

    resetMocks: true,

    testEnvironment: 'jsdom',
};
