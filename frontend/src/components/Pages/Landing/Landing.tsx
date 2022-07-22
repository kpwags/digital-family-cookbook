import useDocumentTitle from '@hooks/useDocumentTitle';

const Landing = () => {
    useDocumentTitle(undefined);

    return (
        <>
            <h1>Home</h1>
            <p>This is the home page</p>
        </>
    );
};

export default Landing;
