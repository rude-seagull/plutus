import { signIn } from 'next-auth/react';
import { SyntheticEvent } from 'react';
import PlutusLogo from '../../components/all/icons/plutusLogo';

const Login = () => {
    function handleSubmit(event: SyntheticEvent) {
        event.preventDefault();
        console.log({
            email: 'event.target[0].value',
            password: 'event.target[1].value',
        });
        signIn('plutus-credential-provider', {
            email: event.target[0].value,
            password: event.target[1].value,
        });
    }

    return (
        <div className="flex flex-col items-center bg-test-light min-h-screen w-full justify-center bg-gray-200">
            <div className="md:max-w-3xl xl:max-w-5xl w-full bg-[#EEEEEE] aspect-[5/3] rounded-md shadow-xl grid grid-cols-2 overflow-hidden">
                <div className="bg-plutus-green flex flex-col p-6 align-middle justify-center gap-2">
                    <PlutusLogo height={250}></PlutusLogo>
                    <h1 className="text-center font-crimson text-7xl text-[#4F5D73] font-semibold">
                        Plutus
                    </h1>
                </div>
                <div className="flex flex-col gap-2 justify-between py-4">
                    <h2 className="text-center text-[3rem]">Log in</h2>

                    <form
                        method="post"
                        action="/api/auth/callback/credentials"
                        className="px-5 flex flex-col gap-4"
                        onSubmit={handleSubmit}
                    >
                        <div>
                            <label className="block text-sm font-medium text-gray-700">
                                Username
                            </label>
                            <input
                                name="username"
                                type="text"
                                className="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 block w-full border-gray-300 px-2"
                            />
                        </div>
                        <div>
                            <label className="block text-sm font-medium text-gray-700">
                                Password
                            </label>
                            <input
                                name="password"
                                type="password"
                                className="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 block w-full border-gray-300 px-2"
                            />
                        </div>

                        <button
                            type="submit"
                            className="bg-plutus-green hover:bg-green-600 py-2 text-white rounded-md shadow-md"
                        >
                            Log in
                        </button>
                    </form>

                    <div className="text-center text-sm text-[#CACACA]">
                        <p>forgot password ? </p>
                        <p>
                            don't have an account ?{' '}
                            <a
                                href=""
                                className="underline decoration-plutus-green decoration-1 hover:text-plutus-green"
                            >
                                Sign Up
                            </a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;
