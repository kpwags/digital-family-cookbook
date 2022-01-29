import { AuthResult } from '@models/AuthResult';

const MockSuccessfulAuthResult: AuthResult = {
    isSuccesful: true,
    error: '',
    token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c',
};

const MockUnsuccessfulAuthResult: AuthResult = {
    isSuccesful: false,
    error: 'Invalid username or password',
    token: '',
};

export {
    MockSuccessfulAuthResult,
    MockUnsuccessfulAuthResult,
};
