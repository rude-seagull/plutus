import { signOut, useSession } from 'next-auth/react';
import { useGetAccounts } from '../../services/account/account';

const SideBar = () => {
    const { data: session, status } = useSession();
    const { data: accounts } = useGetAccounts();

    return (
        <div className="sm:max-w-[12rem] lg:max-w-[15rem] h-screen text-blue-700 p-5 text-xs lg:text-sm border-r overflow-y-auto border-gray-900 ">
            <p
                onClick={() => {
                    signOut();
                }}
            >
                {session?.user?.userName}
            </p>

            {accounts.map((account) => {
                return <p key={`account_${account.id}`}>{account.title}</p>;
            })}
        </div>
    );
};

export default SideBar;
