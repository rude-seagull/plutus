import { useSession } from 'next-auth/react';
import Head from 'next/head';

export default function Home() {
    const { data: session, status } = useSession();
    return (
        <div className="flex flex-col items-center justify-center min-h-screen py-2">
            <Head>
                <title>Create Next App</title>
                <link rel="icon" href="/favicon.ico" />
            </Head>

            <main>
                <p>{session?.user?.userName}</p>
            </main>
        </div>
    );
}
