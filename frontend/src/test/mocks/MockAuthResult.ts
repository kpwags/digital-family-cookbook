import AuthResult from '@models/AuthResult';

const MockSuccessfulAuthResult: AuthResult = {
    isSuccessful: true,
    error: '',
    accessToken: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c',
};

const MockUnsuccessfulAuthResult: AuthResult = {
    isSuccessful: false,
    error: 'Invalid username or password',
    accessToken: '',
};

export {
    MockSuccessfulAuthResult,
    MockUnsuccessfulAuthResult,
};
