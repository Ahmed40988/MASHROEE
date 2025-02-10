INSERT INTO Products (Name, Description, Price, CreatedDate, userid, categoryid, image, Quantity)
VALUES
-- Electronics
('Smart Wireless Earbuds', 'High-quality wireless earbuds with noise cancellation and long battery life.', 150.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 1, '/images/1.jpg', 10),
('4K Ultra HD TV', 'A 55-inch smart TV with vibrant 4K display and built-in streaming apps.', 700.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 1, '/images/2.jpg', 10),
('Gaming Laptop', 'Powerful gaming laptop with Intel Core i7 processor and GTX graphics.', 1200.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 1, '/images/3.jpg', 10),

-- Home & Kitchen
('Stainless Steel Blender', 'A high-speed blender for smoothies, sauces, and more.', 80.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 2, '/images/4.jpg', 10),
('Ceramic Dinner Set', '12-piece ceramic dinner set with elegant design.', 60.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 2, '/images/5.jpg', 10),

-- Fashion & Apparel
('Mens Leather Jacket', 'Stylish and durable leather jacket for men.', 150.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 3, '/images/6.jpg', 10),
('Womens Summer Dress', 'Lightweight and comfortable summer dress.', 45.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 3, '/images/7.jpg', 10),

-- Furniture
('Wooden Dining Table', 'A large, handcrafted dining table made from oak wood.', 300.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 4, '/images/8.jpg', 10),
('Ergonomic Office Chair', 'Adjustable office chair with lumbar support.', 120.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 4, '/images/9.jpg', 10),

-- Clothing
('Cotton T-Shirt', 'Soft and breathable cotton t-shirt available in multiple colors.', 20.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 5, '/images/10.jpg', 10),
('Denim Jeans', 'Classic fit denim jeans for everyday wear.', 35.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 5, '/images/11.jpg', 10),

-- Kitchenware
('Non-Stick Frying Pan', 'Durable non-stick frying pan for healthy cooking.', 25.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 6, '/images/12.jpg', 10),
('Chef\s Knife', 'Premium stainless steel knives for precision cutting.', 50.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 6, '/images/13.jpg', 10),

-- Toys
('Building Blocks', 'Creative building blocks set for kids aged 3 and up.', 30.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 7, '/images/14.jpg', 10),
('Remote Control Car', 'High-speed RC car with rechargeable battery.', 60.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 7, '/images/15.jpg', 10),

-- Books
('Mystery Novel', 'A gripping thriller that will keep you on the edge of your seat.', 15.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 8, '/images/16.jpg', 10),
('Self-Help Guide', 'Practical tips and strategies for personal development.', 20.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 8, '/images/17.jpg', 10),

-- Beauty
('Organic Face Cream', 'Natural moisturizer for soft and radiant skin.', 35.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 9, '/images/18.jpg', 10),
('Perfume Set', 'Luxury perfume set with three signature scents.', 90.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 9, '/images/19.jpg', 10),

-- Sports Equipment
('Yoga Mat', 'Non-slip yoga mat for a comfortable workout.', 25.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 10, '/images/20.jpg', 10),
('Dumbbell Set', 'Adjustable dumbbell set for strength training.', 80.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 10, '/images/21.jpg', 10),

-- Jewelry
('Gold Necklace', 'Elegant gold necklace with intricate design.', 300.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 11, '/images/22.jpg', 10),
('Diamond Ring', 'Exquisite diamond ring for special occasions.', 1500.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 11, '/images/23.jpg', 10),

-- Art
('Abstract Painting', 'Colorful abstract painting for modern interiors.', 250.00, GETDATE(), 'a892c179-3514-4230-8285-cbf89ab951df', 12, '/images/24.jpg', 10),
('Sculpture', 'Handcrafted sculpture made from sustainable materials.', 500.00, GETDATE(), '0f7276a1-9d30-4390-88a9-55cad625c9d9', 12, '/images/25.jpg', 10);
