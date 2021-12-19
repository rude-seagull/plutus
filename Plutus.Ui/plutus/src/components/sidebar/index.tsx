import { signOut, useSession } from 'next-auth/react';

const SideBar = () => {
    const { data: session, status } = useSession();

    return (
        <div className="sm:max-w-[12rem] lg:max-w-[15rem] h-screen text-blue-700 p-5 text-xs lg:text-sm border-r overflow-y-auto border-gray-900 ">
            <p
                onClick={() => {
                    signOut();
                }}
            >
                {session?.user?.userName}
            </p>
        </div>
    );
};

export default SideBar;
