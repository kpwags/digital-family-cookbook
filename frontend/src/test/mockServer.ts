/* eslint-disable import/no-extraneous-dependencies */
import { setupServer } from 'msw/node';
import { mockApiHandlers } from './mockApiHandlers';

const mockServer = setupServer(...mockApiHandlers);

export { mockServer };
