import '../styles/globals.css';
import { SessionProvider } from 'next-auth/react';
import Head from 'next/head';
import { QueryClientProvider, QueryClient } from 'react-query';
import React from 'react';
import { ReactQueryDevtools } from 'react-query/devtools';

function MyApp({ Component, pageProps: { session, ...pageProps } }) {
    const queryClientRef = React.useRef();
    if (!queryClientRef.current) {
        // @ts-ignore
        queryClientRef.current = new QueryClient({
            defaultOptions: {
                queries: {
                    refetchIntervalInBackground: true,
                    refetchOnReconnect: false,
                    refetchOnMount: true,
                    refetchOnWindowFocus: false,
                },
            },
        });
    }

    return (
        <SessionProvider session={session}>
            <QueryClientProvider client={queryClientRef.current}>
                <Head>
                    <link
                        rel="stylesheet"
                        type="text/css"
                        href="//fonts.googleapis.com/css?family=Crimson+Text"
                    />
                </Head>
                <Component {...pageProps} />;
                <ReactQueryDevtools initialIsOpen={false} />
            </QueryClientProvider>
        </SessionProvider>
    );
}

export default MyApp;
