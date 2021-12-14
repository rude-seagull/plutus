import NextAuth, { User } from 'next-auth';
import { JWT } from 'next-auth/jwt';
import { Session } from 'next-auth';
import CredentialsProvider from 'next-auth/providers/credentials';

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

                try {
                    console.log(JSON.stringify(credentials));
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
                        console.log(user);
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
    session: {
        strategy: 'jwt',
    },
    secret: process.env.JWT_SECRET,
    callbacks: {
        async jwt({ token, user, account }) {
            console.log('------ JWT -------');
            // return token;
            if (account && user) {
                console.log('here', token);
                console.log('here', user);
                console.log('here', account);
                return {
                    ...token,
                    accessToken: user.token,
                    // refreshToken: account.refresh_token,
                    username: user.userName,
                    // accessTokenExpires: account.expires_at * 1000, //in millis
                };
            }

            //token still valid
            if (Date.now() < token.accessTokenExpires) {
                console.log('here valid');
                return token;
            }

            //token expired and needs a new one
            console.log('new');
            return token;
        },
        async session({ session, token, user }) {
            // Send properties to the client, like an access_token from a provider.
            console.log('------ SESSION -------');
            console.log(session.user);
            console.log(token);
            console.log(user);
            session.accessToken = token.accessToken;
            session.user.userName = token.username as string;
            return session;
        },
    },
});
