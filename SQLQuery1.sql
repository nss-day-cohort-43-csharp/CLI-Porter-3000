Select  post.id,
                                               post.Title As PostTitle,
                                               post.URL AS PostUrl,
                                               post.PublishDateTime,
                                               post.AuthorId,
                                               post.BlogId,
                                               blog.Title AS BlogTitle,
                                               blog.URL AS BlogUrl
                                          FROM Post post 
                                               LEFT JOIN Blog blog on post.BlogId = blog.Id 
                                         WHERE post.BlogId = 1;