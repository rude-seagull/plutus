import { useSession } from 'next-auth/react';
import Head from 'next/head';
import SideBar from '../components/sidebar';

export default function Home() {
    const { data: session, status } = useSession();
    return (
        <div className={`h-screen overflow-hidden dark:bg-onyx bg-gray-100`}>
            <Head>
                <title>Create Next App</title>
                <link rel="icon" href="/favicon.ico" />
            </Head>

            <main className="flex">
                <SideBar></SideBar>
                <div></div>
            </main>
        </div>
    );
}
