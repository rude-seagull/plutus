import NextAuth from 'next-auth';
import { JWT } from 'next-auth/jwt';

declare module 'next-auth' {
    interface Session {
        user: {
            email: string;
            userName: string;
            roles: string[];
            token: string;
        };

        error: string;
    }
}

declare module 'next-auth/jwt' {
    interface JWT {
        email: string;
        userName: string;
        roles: string[];
        token: string;
    }
}
