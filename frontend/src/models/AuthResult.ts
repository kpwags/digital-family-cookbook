interface AuthResult {
    refreshToken: string
    accessToken: string
    isSuccessful: boolean
    error: string
}

export default AuthResult;
