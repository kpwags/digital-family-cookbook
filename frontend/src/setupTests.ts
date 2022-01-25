/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable consistent-return */
/* eslint-disable import/no-extraneous-dependencies */
import '@testing-library/jest-dom';

import { server } from './test/server';

// eslint-disable-next-line func-names
window.matchMedia = window.matchMedia || function () {
    return {
        matches: false,
        addEventListener: jest.fn(),
        addListener: jest.fn(),
        removeEventListener: jest.fn(),
        removeListener: jest.fn(),
    };
};

window.scrollTo = jest.fn();

const { warn, error } = console;

beforeAll(() => {
    server.listen();

    const ignoredWarnings: string[] = [
        'async-validator:',
    ];

    const ignoredErrors: string[] = [
        'async-validator:',
    ];

    const isMessageIncluded = (msg: string, ignoreList: string[]): boolean => {
        let isFound = false;

        ignoreList.forEach((w) => {
            if (msg.indexOf(w) !== -1) {
                isFound = true;
            }
        });

        return isFound;
    };

    // eslint-disable-next-line no-console
    console.warn = (...args: any[]) => {
        // Material UI grids throw a warning because the component is loading in a mocked DOM
        if (typeof args[0] === 'string' && isMessageIncluded(args[0], ignoredWarnings)) {
            return false;
        }

        warn(...args);
    };

    // eslint-disable-next-line no-console
    console.error = (...args: any[]) => {
        // Material UI grids throw a warning because the component is loading in a mocked DOM
        if (typeof args[0] === 'string' && isMessageIncluded(args[0], ignoredErrors)) {
            return false;
        }

        if (typeof args[2] === 'string' && isMessageIncluded(args[2], ignoredErrors)) {
            return false;
        }

        error(...args);
    };
});

// if you need to add a handler after calling setupServer for some specific test
// this will remove that handler for the rest of them
// (which is important for test isolation):
afterEach(() => server.resetHandlers());
afterAll(() => {
    server.close();

    // eslint-disable-next-line no-console
    console.warn = warn;
});
