import { BuiltInProviderType } from 'next-auth/providers';
import {
    ClientSafeProvider,
    getProviders,
    LiteralUnion,
    signIn,
} from 'next-auth/react';

interface props {
    providers: Record<
        LiteralUnion<BuiltInProviderType, string>,
        ClientSafeProvider
    >;
}

const Login = ({ providers }: props) => {
    return (
        <div className="flex flex-col items-center bg-black min-h-screen w-full justify-center">
            <img
                className="w-52 mb-5"
                src="https://cdn.icon-icons.com/icons2/836/PNG/512/Spotify_icon-icons.com_66783.png"
                alt=""
            />
            {Object.values(providers).map((provider) => {
                return (
                    <div key={provider.name}>
                        <button
                            className="bg-[#18D860] text-white p-5 rounded-full"
                            onClick={() =>
                                signIn(provider.id, { callbackUrl: '/' })
                            }
                        >
                            Login with {provider.name}
                        </button>
                    </div>
                );
            })}
        </div>
    );
};

export default Login;

export async function getServerSideProps(ctx) {
    const providers = await getProviders();
    return {
        props: { providers },
    };
}
