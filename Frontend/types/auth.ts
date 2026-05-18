export interface LoginResponse{
    token: string;
    expiration: string;
    username: string;
    roles: string[];
}

export interface UserCredentials{
    username: string;
    password: string;
}