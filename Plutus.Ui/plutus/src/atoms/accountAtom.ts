import { atom } from 'recoil';
import { AccountResponse } from '../types/dtos';

export const accountIdState = atom({
    key: 'accountIdAtomState',
    default: '',
});

export const activeAccountState = atom<AccountResponse>({
    key: 'activeAccountAtomState',
    default: {},
});
