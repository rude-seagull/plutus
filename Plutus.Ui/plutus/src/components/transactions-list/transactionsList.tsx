import { useEffect, useState } from 'react';
import { useRecoilState } from 'recoil';
import { accountIdState, activeAccountState } from '../../atoms/accountAtom';
import { getTransactions } from '../../services/transaction/transaction';
import { TransactionResponse } from '../../types/dtos';

const TransactionsList = () => {
    const [accountId, setAccountId] = useRecoilState(accountIdState);
    const [account, setAccount] = useRecoilState(activeAccountState);
    const [transactions, setTransactions] = useState<TransactionResponse[]>([]);

    useEffect(() => {
        console.log('you clicked on: ' + account.id);
        if (account?.id)
            getTransactions(account.id).then((transactions) => {
                setTransactions(transactions);
                console.log(transactions);
            });
    }, [account]);

    return (
        <div>
            <h2>
                {account?.title} : {account?.balance}
            </h2>
            {transactions?.map((tr) => {
                const { amount, id } = tr;
                return (
                    <div
                        key={`tr_item_${id}`}
                        className={
                            amount > 0 ? `text-green-500` : `text-red-500`
                        }
                    >
                        {amount}
                    </div>
                );
            })}
        </div>
    );
};

export default TransactionsList;
