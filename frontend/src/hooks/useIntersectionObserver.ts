/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useRef } from 'react';

const useIntersectionObserver = (selectors: string, setElementVisible: (val: string) => void): any => {
    const elementsRef = useRef<any>({});

    useEffect(() => {
        const selectedElements = Array.from(document.querySelectorAll(selectors)) as HTMLElement[];

        const callback = (headings: IntersectionObserverEntry[]) => {
            elementsRef.current = headings.reduce((map, headingElement) => {
                // eslint-disable-next-line no-param-reassign
                map[headingElement.target.id] = headingElement;

                return map;
            }, elementsRef.current);

            const visibleElements: any = [];

            Object.keys(elementsRef.current).forEach((key) => {
                const headingElement = elementsRef.current[key];
                if (headingElement.isIntersecting) {
                    visibleElements.push(headingElement);
                }
            });

            const getIndexFromId = (id: string): number => selectedElements.findIndex((element) => element.id === id);

            if (visibleElements.length === 1) {
                setElementVisible(visibleElements[0].target.id);
            } else if (visibleElements.length > 1) {
                const sortedVisibleHeadings = visibleElements.sort(
                    (a: any, b: any) => getIndexFromId(a.target.id) > getIndexFromId(b.target.id),
                );
                setElementVisible(sortedVisibleHeadings[0].target.id);
            }
        };

        const observer = new IntersectionObserver(callback, {
            rootMargin: '-110px 0px -40% 0px',
        });

        selectedElements.forEach((element) => observer.observe(element));

        return () => observer.disconnect();
    }, []);
};

export default useIntersectionObserver;
