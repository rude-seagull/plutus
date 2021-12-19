import NextAuth, { User } from 'next-auth';
import { JWT } from 'next-auth/jwt';
import CredentialsProvider from 'next-auth/providers/credentials';
import jwt_decode from 'jwt-decode';
import { useRouter } from 'next/router';

interface test {
    sub: string;
    jti: string;
    email: string;
    uid: string;
    roles: string;
    exp: number;
    iss: string;
    aud: string;
}

export default NextAuth({
    // Configure one or more authentication providers
    providers: [
        CredentialsProvider({
            id: 'plutus-credential-provider',
            name: 'Credentials',
            credentials: {
                email: {
                    label: 'email',
                    type: 'text',
                    placeholder: 'jsmith@societe.com',
                },
                password: { label: 'Password', type: 'password' },
            },
            type: 'credentials',
            async authorize(credentials, req) {
                // const user = { id: 1, name: "J Smith", email: "jsmith@example.com" }
                // return user;
                // console.log('------ LOGIN -------');
                try {
                    // console.log(JSON.stringify(credentials));
                    const res = await fetch(
                        'http://localhost:5042/api/users/authenticate',
                        {
                            method: 'POST',
                            body: JSON.stringify(credentials),
                            headers: { 'Content-Type': 'application/json' },
                        },
                    );
                    const user = (await res.json()) as User;

                    // If no error and we have user data, return it
                    if (res.ok && user) {
                        return user;
                    }
                    // Return null if user data could not be retrieved
                    return null;
                } catch (e) {
                    console.error(e);
                    return null;
                }
            },
        }),
        // ...add more providers here
    ],
    pages: {
        signIn: '/login',
        error: '/login',
    },
    session: {
        strategy: 'jwt',
    },
    secret: process.env.JWT_SECRET,
    callbacks: {
        async jwt({ token, user, account, profile }) {
            // console.log('------ JWT -------');
            // return token;

            // console.log(token);
            // console.log(user);

            if (account && user) {
                let customToken = jwt_decode(user.token as string) as test;
                // console.log(customToken);
                // console.warn('create', {
                //     ...token,
                //     accessToken: user.token,
                //     // refreshToken: account.refresh_token,
                //     username: user.userName,
                //     roles: user.roles,
                //     exp: customToken.exp as number, //in millis
                //     expires: customToken.exp as number, //in millis
                // } as JWT);

                return {
                    ...token,
                    accessToken: user.token,
                    // refreshToken: account.refresh_token,
                    username: user.userName,
                    roles: user.roles,
                    exp: customToken.exp as number, //in millis
                    expires: (customToken.exp as number) * 1000, //in millis
                } as JWT;
            }

            //token still valid
            // console.log(
            //     'TOUJOURS VALIDE ? ',
            //     Date.now() < token.expires,
            //     Date.now(),
            //     token.expires,
            // );
            if (Date.now() < token.expires) {
                // console.log('here valid', token);
                return token;
            }

            //token expired and needs a new one
            //TODO: redirect to login page in middleware
            // console.log('new', token);
            return token;
        },
        async session({ session, token, user }) {
            // Send properties to the client, like an access_token from a provider.
            // console.log('------ SESSION -------');
            // console.log(token);
            session.accessToken = token.accessToken;
            session.user.userName = token.username as string;
            session.user.roles = token.roles;
            // console.log(session);
            return session;
        },
        redirect({ url, baseUrl }) {
            return baseUrl;
        },
    },
});
