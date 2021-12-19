module.exports = {
    // darkMode: 'class',
    mode: 'jit',
    content: [
        './src/pages/**/*.{js,ts,jsx,tsx}',
        './src/components/**/*.{js,ts,jsx,tsx}',
    ],
    theme: {
        extend: {
            colors: {
                'regal-blue': '#243c5a',
                onyx: '#373A40',
                'plutus-green': '#4ECCA3',
                'plutus-green-dark': '#3ac6b4',
            },
            fontFamily: {
                crimson: ['Crimson\\ Text'],
            },
        },
    },
    plugins: [],
};
