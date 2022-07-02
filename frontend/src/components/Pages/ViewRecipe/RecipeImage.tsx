import { PageState } from '@utils/constants';
import { useEffect, useState } from 'react';
import { Api } from '@utils/api';
import { Alert, Spin } from 'antd';

type RecipeImageProps = {
    filename?: string
    imageData?: string
    recipeName: string
}

const RecipeImage = ({
    filename = '',
    imageData = '',
    recipeName = 'Recipe',
}: RecipeImageProps): JSX.Element => {
    const [pageState, setPageState] = useState<PageState>(PageState.Loading);
    const [errorMessage, setErrorMessage] = useState<string>('');
    const [image, setImage] = useState<string>('');

    const fetchImage = async () => {
        const [data, error] = await Api.Get<string>('recipes/getimage', { params: { filename } });

        if (error) {
            setErrorMessage(error);
            setPageState(PageState.Error);
        }

        setImage(data || '');
        setPageState(PageState.Ready);
    };

    useEffect(() => {
        if (imageData !== '') {
            setImage(imageData);
            setPageState(PageState.Ready);
        } else if (filename !== '') {
            fetchImage();
        } else {
            setErrorMessage('No image provided');
            setPageState(PageState.Error);
        }
    }, []);

    if (pageState === PageState.Loading) {
        return <Spin />;
    }

    if (pageState === PageState.Error) {
        return <Alert message={errorMessage} type="error" />;
    }

    return (
        <img
            src={image}
            alt={recipeName}
            className="recipe-image"
            data-testid="recipe-image"
        />
    );
};

export default RecipeImage;
