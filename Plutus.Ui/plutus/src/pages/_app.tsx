import '../styles/globals.css';
import { SessionProvider } from 'next-auth/react';
import Head from 'next/head';

function MyApp({ Component, pageProps: { session, ...pageProps } }) {
    return (
        <SessionProvider session={session}>
            <Head>
                <link
                    rel="stylesheet"
                    type="text/css"
                    href="//fonts.googleapis.com/css?family=Crimson+Text"
                />
            </Head>
            <Component {...pageProps} />;
        </SessionProvider>
    );
}

export default MyApp;
