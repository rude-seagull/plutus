import { signOut, useSession } from 'next-auth/react';
import { useRouter } from 'next/router';
import { useGetAccounts } from '../../services/account/account';
import { useRecoilState } from 'recoil';
import { accountIdState, activeAccountState } from '../../atoms/accountAtom';
import { useEffect, useState } from 'react';
import { ChevronDown, ChevronLeft } from '../all/icons';

const SideBar = () => {
    const router = useRouter();

    const { data: session, status } = useSession();
    const { data: accounts } = useGetAccounts();
    const [accountId, setAccountId] = useRecoilState(accountIdState);
    const [account, setAccount] = useRecoilState(activeAccountState);

    const [isOpen, setIsOpen] = useState<boolean>(false);

    useEffect(() => {
        console.log('you clicked on: ' + accountId);
    }, [accountId]);

    return (
        <nav className="px-5 bg-slate-600 h-screen select-none sm:w-[12rem] lg:w-[15rem] text-[#fff]">
            <ul>
                <li>
                    <p
                        className="block py-2 px-4 hover:bg-[#1d4f71]"
                        onClick={async () => {
                            signOut();
                        }}
                    >
                        User: {session?.user?.userName}
                    </p>
                </li>
                <li>
                    <p
                        className="py-2 px-4 hover:bg-[#1d4f71] flex items-center justify-between cursor-pointer"
                        onClick={() => {
                            setIsOpen(!isOpen);
                        }}
                    >
                        Accounts
                        {isOpen ? (
                            <ChevronDown stroke="black" height="1em" />
                        ) : (
                            <ChevronLeft stroke="black" height="1em" />
                        )}
                    </p>
                    <ul
                        className={`${
                            isOpen ? 'block' : 'hidden'
                        } bg-opacity-20 bg-white`}
                    >
                        {accounts?.map((account) => {
                            return (
                                <li
                                    className="cursor-pointer"
                                    key={`account_${account.id}`}
                                    onClick={() => {
                                        router.replace(
                                            ``,
                                            `/account/${account.id}`,
                                            { shallow: true },
                                        );
                                        setAccount(account);
                                    }}
                                >
                                    <p className="border-l-4 border-solid border-transparent hover:border-plutus-green py-2 px-4 ml-8 hover:bg-[#1d4f71]">
                                        {account.title}
                                    </p>
                                </li>
                            );
                        })}
                    </ul>
                </li>
            </ul>
        </nav>

        // <ul className="sm:max-w-[12rem] lg:max-w-[15rem] h-screen text-gray-700 p-5 text-xs lg:text-sm border-r overflow-y-auto border-gray-900 ">
        //     <li className="border-b border-gray-600 hover:text-plutus-green cursor-pointer hover:border-plutus-green">
        //         <p
        //             onClick={async () => {
        //                 signOut();
        //             }}
        //         >
        //             {session?.user?.userName}
        //         </p>
        //     </li>

        //     {accounts?.map((account) => {
        //         return (
        //             <p
        //                 key={`account_${account.id}`}
        //                 onClick={() => {
        //                     setAccountId(account.id);
        //                 }}
        //             >
        //                 {account.title}
        //             </p>
        //         );
        //     })}
        // </ul>
    );
};

export default SideBar;
